import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { CidadeFavorita } from '../models/models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class FavoritesService {
  private apiUrl = environment.apiUrl + '/favorito'; // Ajuste conforme o backend
  private favoritesSubject = new BehaviorSubject<CidadeFavorita[]>([]);
  favorites$ = this.favoritesSubject.asObservable();

  constructor(private http: HttpClient) {
    this.loadFavorites();
  }

  loadFavorites(): void {
    this.http.get<CidadeFavorita[]>(`${this.apiUrl}s`).subscribe((favs) => {
      this.favoritesSubject.next(favs);
    });
  }

  getFavorites(): Observable<CidadeFavorita[]> {
    return this.http.get<CidadeFavorita[]>(`${this.apiUrl}s`);
  }

  addFavorite(city: string): Observable<any> {
    const favorito = { nomeCidade: city };
    return this.http.post(this.apiUrl, favorito);
  }

  removeFavorite(city: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${city}`);
  }
}
