import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  standalone: true,
})
export class LoginComponent {
  email: string = '';
  password: string = '';
  errorMessage: string = '';

  constructor(
    private authService: AuthenticationService,
    private router: Router
  ) {}

  async login() {
    try {
      await this.authService.login(this.email, this.password);
      this.router.navigate(['/lista-cidade']);
    } catch (error) {
      this.errorMessage = 'Invalid credentials';
    }
  }

  async registro() {
    try {
      await this.authService.cadastrar(this.email, this.password);
      this.router.navigate(['/lista-cidade']);
    } catch (error) {
      this.errorMessage = 'Registration failed';
    }
  }
}
