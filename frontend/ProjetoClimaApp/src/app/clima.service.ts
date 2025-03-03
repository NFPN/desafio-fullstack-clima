import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ClimaService {
  private apiUrl = 'http://localhost:5000';

  constructor(private http: HttpClient) {}

  getClima(cidade: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/clima/${cidade}`);
  }

  getPrevisao(cidade: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/previsao/${cidade}`);
  }
}
