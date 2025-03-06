import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, BehaviorSubject } from 'rxjs';
import { LoginModel, RegistroModel } from '../models/models';
import { environment } from '../../environments/environment';
import { FavoritesService } from './favorites.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = environment.apiUrl + '/auth';
  private loginStatusSubject = new BehaviorSubject<boolean>(this.isLoggedIn());
  loginStatus$ = this.loginStatusSubject.asObservable();

  constructor(
    private http: HttpClient,
    private router: Router,
    private favoritesService: FavoritesService // Injetar o FavoritesService
  ) {}

  login(model: LoginModel): Observable<any> {
    return new Observable((observer) => {
      this.http
        .post<{ token: string }>(`${this.apiUrl}/login`, model)
        .subscribe({
          next: (response) => {
            this.saveToken(response.token);
            this.loginStatusSubject.next(true);
            this.favoritesService.clearAndReloadFavorites(); // Limpa e recarrega os favoritos
            observer.next(response);
            observer.complete();
          },
          error: (err) => observer.error(err),
        });
    });
  }

  register(model: RegistroModel): Observable<any> {
    return this.http.post(`${this.apiUrl}/registro`, model);
  }

  saveToken(token: string): void {
    localStorage.setItem('token', token);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('favorites');
    this.loginStatusSubject.next(false);
    this.favoritesService.clearAndReloadFavorites(); // Limpa e recarrega os favoritos
    this.router.navigate(['/login']);
  }
}
