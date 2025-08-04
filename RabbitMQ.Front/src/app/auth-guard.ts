import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { AuthService } from './auth.service';

export const authGuard: CanActivateFn = (route, state) => {
  const auth = inject(AuthService);

  if (!auth.isLoggedIn) {
    auth.logout();
    return false;
  }

  const requiredRoles: string[] = route.data?.['roles'] || [];

  if (requiredRoles.length === 0) {
    return true;
  }

  if (auth.role && requiredRoles.includes(auth.role)) {
    return true;
  }

  auth.logout();
  return false;
};
