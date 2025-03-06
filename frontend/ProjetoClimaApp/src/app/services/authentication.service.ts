import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  private apiUrl = 'https://localhost:5001/auth';
  private tokenKey = 'jwt_token';
  private userSubject = new BehaviorSubject<string | null>(null);

  constructor(private http: HttpClient) {
    const token = localStorage.getItem(this.tokenKey);
    if (token) {
      this.userSubject.next(this.getUserIdFromToken(token));
    }
  }

  async login(email: string, password: string): Promise<void> {
    const response = await firstValueFrom(
      this.http.post<{ token: string }>(`${this.apiUrl}/login`, {
        email,
        password,
      })
    );
    const token = response?.token;
    if (token) {
      localStorage.setItem(this.tokenKey, token);
      this.userSubject.next(this.getUserIdFromToken(token));
    }
  }

  async cadastrar(email: string, password: string): Promise<void> {
    await firstValueFrom(
      this.http.post(`${this.apiUrl}/register`, { email, password })
    );
    await this.login(email, password);
  }

  logout() {
    localStorage.removeItem(this.tokenKey);
    this.userSubject.next(null);
  }

  obterToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  private getUserIdFromToken(token: string): string {
    const payload = JSON.parse(atob(token.split('.')[1]));
    return payload[
      'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'
    ];
  }
}
