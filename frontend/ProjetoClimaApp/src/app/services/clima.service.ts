import { Injectable } from '@angular/core';
import { DadosClima } from '../models/DadosClima.model';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';
import { DadosPrevisao } from '../models/DadosPrevisao';

@Injectable({
  providedIn: 'root',
})
export class ClimaService {
  private apiUrl = 'https://localhost:7018';

  constructor(private http: HttpClient) {}

  async obterClima(cidade: string): Promise<DadosClima> {
    return await firstValueFrom(
      this.http.get<DadosClima>(`${this.apiUrl}/clima/${cidade}`)
    );
  }

  async obterPrevisao(cidade: string): Promise<DadosPrevisao> {
    return await firstValueFrom(
      this.http.get<DadosPrevisao>(`${this.apiUrl}/previsao/${cidade}`)
    );
  }
}
