import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { WeatherService } from '../services/weather.service';
import { DadosClima, DadosPrevisao } from '../models/models';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-forecast',
  templateUrl: './forecast.component.html',
  standalone: true,
  imports: [CommonModule],
})
export class ForecastComponent {
  city: string | null = null;
  weather: DadosClima | null = null;
  forecast: DadosPrevisao | null = null;

  constructor(
    private route: ActivatedRoute,
    private weatherService: WeatherService
  ) {
    this.city = this.route.snapshot.paramMap.get('city');
    if (this.city) {
      this.fetchData(this.city);
    }
  }

  fetchData(city: string): void {
    this.weatherService.getWeather(city).subscribe({
      next: (data) => (this.weather = data),
      error: (err) => console.error('Erro ao buscar dados do tempo:', err),
    });
    this.weatherService.getForecast(city).subscribe({
      next: (data) => (this.forecast = data),
      error: (err) => console.error('Erro ao buscar previsão:', err),
    });
  }
}
