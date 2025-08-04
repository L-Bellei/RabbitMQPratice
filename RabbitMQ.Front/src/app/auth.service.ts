import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private get isBrowser(): boolean {
    return typeof window !== 'undefined' && !!window.sessionStorage;
  }

  get token(): string | null {
    if (!this.isBrowser) return null;
    return sessionStorage.getItem('token');
  }

  get user(): any | null {
    if (!this.isBrowser) return null;

    const u = sessionStorage.getItem('user');

    return u ? JSON.parse(u) : null;
  }

  get role(): string | null {
    return this.user?.role ?? null;
  }

  get isLoggedIn(): boolean {
    return !!this.token && !!this.user;
  }

  login(token: string, user: any): void {
    if (!this.isBrowser) return;

    sessionStorage.setItem('token', token);
    sessionStorage.setItem('user', JSON.stringify(user));
  }

  logout(): void {
    if (!this.isBrowser) return;

    sessionStorage.removeItem('token');
    sessionStorage.removeItem('user');

    window.location.href = '/';
  }
}
