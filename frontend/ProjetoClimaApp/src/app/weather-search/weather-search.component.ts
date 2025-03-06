import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { WeatherService } from '../services/weather.service';
import { FavoritesService } from '../services/favorites.service';
import { AuthService } from '../services/auth.service';
import { DadosClima, DadosPrevisao, CidadeFavorita } from '../models/models';

@Component({
  selector: 'app-weather-search',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './weather-search.component.html',
})
export class WeatherSearchComponent {
  city: string = '';
  weather: DadosClima | null = null;
  forecast: DadosPrevisao | null = null;
  favorites: CidadeFavorita[] = [];
  weatherData: { [key: string]: DadosClima } = {};
  errorMessage: string | null = null;

  constructor(
    private weatherService: WeatherService,
    private favoritesService: FavoritesService,
    private authService: AuthService
  ) {
    this.favoritesService.favorites$.subscribe((favs) => {
      this.favorites = favs;
      favs.forEach((fav) => {
        this.weatherService.getWeather(fav.nomeCidade).subscribe({
          next: (data) => (this.weatherData[fav.nomeCidade] = data),
          error: (err) =>
            console.error(`Erro ao carregar clima de ${fav.nomeCidade}:`, err),
        });
      });
    });
  }

  searchWeather(): void {
    this.weatherService.getWeather(this.city).subscribe({
      next: (data) => (this.weather = data),
      error: (err) => (this.errorMessage = err.message),
    });
    this.weatherService.getForecast(this.city).subscribe({
      next: (data) => (this.forecast = data),
      error: (err) => (this.errorMessage = err.message),
    });
  }

  isFavorite(city: string): boolean {
    return this.favorites.some((f) => f.nomeCidade === city);
  }

  toggleFavorite(city: string): void {
    if (!this.authService.isLoggedIn()) {
      this.errorMessage = 'Faça login para gerenciar favoritos.';
      return;
    }
    if (this.isFavorite(city)) {
      this.removeFavorite(city);
    } else {
      this.favoritesService.addFavorite(city).subscribe({
        next: () => this.favoritesService.loadFavorites(),
        error: (err) =>
          (this.errorMessage = 'Erro ao adicionar favorito: ' + err.message),
      });
    }
  }

  removeFavorite(city: string): void {
    this.favoritesService.removeFavorite(city).subscribe({
      next: () => this.favoritesService.loadFavorites(),
      error: (err) =>
        (this.errorMessage = 'Erro ao remover favorito: ' + err.message),
    });
  }
}
