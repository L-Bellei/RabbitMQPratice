import { Routes } from '@angular/router';
import { LoginComponent } from './login/login';
import { HomeComponent } from './home/home';
import { authGuard } from './auth-guard';
import { ListComponent } from './user/list/list';
import { CreateComponent } from './user/create/create';
import { EditComponent } from './user/edit/edit';
import { DetailsComponent } from './user/details/details';

export const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'home', component: HomeComponent, canActivate: [authGuard] },

  {
    path: 'users',
    component: ListComponent,
    canActivate: [authGuard],
    data: { roles: ['Admin', 'Manager'] },
  },
  {
    path: 'users/create',
    component: CreateComponent,
    canActivate: [authGuard],
    data: { roles: ['Admin'] },
  },
  {
    path: 'users/edit/:id',
    component: EditComponent,
    canActivate: [authGuard],
    data: { roles: ['Admin', 'Manager'] },
  },
  {
    path: 'users/details/:id',
    component: DetailsComponent,
    canActivate: [authGuard],
    data: { roles: ['Admin', 'Manager'] },
  },
  { path: '**', redirectTo: '' },
];
