import { Routes } from '@angular/router';
import { LoginComponent } from './modules/auth/components/login/login.component';
import { RegisterComponent } from './modules/auth/components/register/register.component';
import { NotAuthGuard } from './guards/not-auth.guard';
import { HomeComponent } from './modules/home/components/home/home.component';
import { AuthGuard } from './guards/auth.guard';
import { TransacionLayoutComponent } from './modules/transaction/components/transacion-layout/transacion-layout.component';

export const routes: Routes = [
  { path: '', redirectTo: 'home/transactions', pathMatch: 'full' },
  { path: 'home', redirectTo: 'home/transactions', pathMatch: 'full' },
  {
    path: 'home',
    component: HomeComponent,
    canActivate: [AuthGuard],
    children: [{ path: 'transactions', component: TransacionLayoutComponent }],
  },
  {
    path: 'login',
    component: LoginComponent,
    canActivate: [NotAuthGuard],
  },
  {
    path: 'register',
    component: RegisterComponent,
    canActivate: [NotAuthGuard],
  },
];
