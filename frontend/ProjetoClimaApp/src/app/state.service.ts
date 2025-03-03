import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class StateService {
  private cidadeSubject = new BehaviorSubject<string>('');
  cidade$ = this.cidadeSubject.asObservable();

  setCidadeAtual(cidade: string) {
    this.cidadeSubject.next(cidade);
  }
}
