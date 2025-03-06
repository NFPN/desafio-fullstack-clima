import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FavoritesService } from '../services/favorites.service';
import { WeatherService } from '../services/weather.service';
import { CidadeFavorita, DadosClima } from '../models/models';

@Component({
  selector: 'app-favorites',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './favorites.component.html',
})
export class FavoritesComponent {
  favorites: CidadeFavorita[] = [];
  weatherData: { [key: string]: DadosClima } = {};

  constructor(
    private favoritesService: FavoritesService,
    private weatherService: WeatherService
  ) {
    this.favoritesService.favorites$.subscribe((favs) => {
      this.favorites = favs;
      favs.forEach((fav) => {
        this.weatherService.getWeather(fav.nomeCidade).subscribe((data) => {
          this.weatherData[fav.nomeCidade] = data;
        });
      });
    });
  }

  removeFavorite(city: string): void {
    this.favoritesService
      .removeFavorite(city)
      .subscribe(() => this.favoritesService.loadFavorites());
  }
}
