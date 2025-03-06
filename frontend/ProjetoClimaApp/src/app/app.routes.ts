import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { ListaCidadeComponent } from './components/lista-cidade/lista-cidade.component';
import { AuthGuard } from './guards/auth.guard';

export const appRoutes: Routes = [
  { path: 'login', component: LoginComponent },
  {
    path: 'lista-cidades',
    component: ListaCidadeComponent,
    canActivate: [AuthGuard],
  },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: '**', redirectTo: '/login' },
];
