import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class FavoritoService {
  private favoritosKey = 'favoritos';
  private favoritosSubject = new BehaviorSubject<string[]>(
    this.obterFavoritosDoStorage()
  );

  favoritos$ = this.favoritosSubject.asObservable();

  private obterFavoritosDoStorage(): string[] {
    const favoritos = localStorage.getItem(this.favoritosKey);
    return favoritos ? JSON.parse(favoritos) : [];
  }

  private salvarFavoritosNoStorage(favoritos: string[]) {
    localStorage.setItem(this.favoritosKey, JSON.stringify(favoritos));
  }

  adicionarFavorito(cidade: string) {
    const favoritos = this.favoritosSubject.value;
    if (!favoritos.includes(cidade)) {
      favoritos.push(cidade);
      this.salvarFavoritosNoStorage(favoritos);
      this.favoritosSubject.next(favoritos);
    }
  }

  removerFavorito(cidade: string) {
    const favoritos = this.favoritosSubject.value;
    const novoFavoritos = favoritos.filter((fav: string) => fav !== cidade);
    this.salvarFavoritosNoStorage(novoFavoritos);
    this.favoritosSubject.next(novoFavoritos);
  }
}
