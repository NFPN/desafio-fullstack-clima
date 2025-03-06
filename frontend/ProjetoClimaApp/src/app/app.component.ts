import { Component, ViewChild } from '@angular/core';
import { RouterOutlet, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { NavComponent } from './nav/nav.component';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule, NavComponent],
  templateUrl: './app.component.html',
  styles: [],
})
export class AppComponent {
  @ViewChild('navComponent') navComponent?: NavComponent;

  constructor(private authService: AuthService, private router: Router) {}

  shouldShowNav(): boolean {
    const currentRoute = this.router.url.split('?')[0];
    const protectedRoutes = ['/search', '/favorites', '/forecast/'];
    return (
      this.authService.isLoggedIn() &&
      protectedRoutes.some((route) => currentRoute.startsWith(route))
    );
  }
}
