import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { tap } from 'rxjs/operators'; // Importar o operador tap
import { CidadeFavorita } from '../models/models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class FavoritesService {
  private apiUrl = environment.apiUrl + '/favorito';
  private favoritesSubject = new BehaviorSubject<CidadeFavorita[]>([]);
  favorites$ = this.favoritesSubject.asObservable();
  private readonly LOCAL_STORAGE_KEY = 'favorites';

  constructor(private http: HttpClient) {
    this.loadFavorites();
  }

  loadFavorites(): void {
    const localFavorites = localStorage.getItem(this.LOCAL_STORAGE_KEY);
    if (localFavorites) {
      const favorites: CidadeFavorita[] = JSON.parse(localFavorites);
      this.favoritesSubject.next(favorites);
    } else {
      this.loadFavoritesFromBackend();
    }
  }

  getFavorites(): Observable<CidadeFavorita[]> {
    return this.http.get<CidadeFavorita[]>(`${this.apiUrl}s`);
  }

  addFavorite(city: string): Observable<any> {
    const favorito = { nomeCidade: city };
    return this.http.post(this.apiUrl, favorito).pipe(
      tap(() => this.loadFavoritesFromBackend()) // Atualiza após sucesso
    );
  }

  removeFavorite(city: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${city}`).pipe(
      tap(() => this.loadFavoritesFromBackend()) // Atualiza após sucesso
    );
  }

  private loadFavoritesFromBackend(): void {
    this.http.get<CidadeFavorita[]>(`${this.apiUrl}s`).subscribe((favs) => {
      this.favoritesSubject.next(favs);
      localStorage.setItem(this.LOCAL_STORAGE_KEY, JSON.stringify(favs));
    });
  }

  clearAndReloadFavorites(): void {
    localStorage.removeItem(this.LOCAL_STORAGE_KEY);
    this.favoritesSubject.next([]);
    this.loadFavoritesFromBackend();
  }
}
