import { RouterModule, Routes } from '@angular/router';
import { Login } from './pages/login/login';
import { Register } from './pages/register/register';
import { Home } from './pages/home/home';
import { authGuard } from './core/guards/auth/auth.guard';
import { Panel } from './layout/panel/panel';
import { Quinielas } from './pages/quinielas/quinielas';
import { Admin } from './pages/admin/admin';

export const routes: Routes = [
    
  { path: '', redirectTo: 'login', pathMatch: 'full' },

  {
    path: 'login',
    component: Login
  },

  {
    path: 'register',
    component: Register
  },

  {
    path: '',
    component: Panel,
    canActivate: [authGuard],
    children: [
        { path: 'home', component: Home},
        { path: 'quinielas', component: Quinielas},
        { path: 'admin', component: Admin}
    ]
  }
];


