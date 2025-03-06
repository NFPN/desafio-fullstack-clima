import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DadosClima, DadosPrevisao } from '../models/models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class WeatherService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getWeather(city: string): Observable<DadosClima> {
    return this.http.get<DadosClima>(`${this.apiUrl}/clima/${city}`);
  }

  getForecast(city: string): Observable<DadosPrevisao> {
    return this.http.get<DadosPrevisao>(`${this.apiUrl}/previsao/${city}`);
  }
}
