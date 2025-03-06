import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { CidadeFavorita } from '../models/CidadeFavorita';

@Injectable({
  providedIn: 'root',
})
export class CidadeStateService {
  private cidadeSubject = new BehaviorSubject<CidadeFavorita[]>([]);
  favorites$ = this.cidadeSubject.asObservable();

  setFavorites(favorites: CidadeFavorita[]) {
    this.cidadeSubject.next(favorites);
  }
}
