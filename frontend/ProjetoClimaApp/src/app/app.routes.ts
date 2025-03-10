import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { WeatherSearchComponent } from './weather-search/weather-search.component';
import { FavoritesComponent } from './favorites/favorites.component';
import { ForecastComponent } from './forecast/forecast.component';
import { AuthGuard } from './auth.guard';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'search', component: WeatherSearchComponent },
  {
    path: 'favorites',
    component: FavoritesComponent,
    canActivate: [AuthGuard],
  },
  { path: 'forecast/:city', component: ForecastComponent },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
];
