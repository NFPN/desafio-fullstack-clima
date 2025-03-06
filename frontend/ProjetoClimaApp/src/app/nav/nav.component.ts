import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { BreakpointObserver, BreakpointState } from '@angular/cdk/layout'; // Corrigido para @angular/cdk/layout

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive],
  templateUrl: './nav.component.html',
})
export class NavComponent {
  isCollapsed: boolean = false; // Inicializado
  isLargeScreen: boolean = false;

  constructor(
    private authService: AuthService,
    private breakpointObserver: BreakpointObserver
  ) {
    this.breakpointObserver
      .observe(['(min-width: 992px)'])
      .subscribe((result: BreakpointState) => {
        this.isLargeScreen = result.matches;
        this.isCollapsed = !this.isLargeScreen; // Escondida por padrão em telas pequenas
      });
  }

  isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }

  logout(): void {
    this.authService.logout();
  }

  toggleCollapse(): void {
    this.isCollapsed = !this.isCollapsed;
  }

  shouldShowNav(): boolean {
    return this.isLoggedIn();
  }
}
