import { NgModule } from '@angular/core';
import {
  BrowserModule,
  provideClientHydration,
} from '@angular/platform-browser';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideHttpClient, withInterceptors } from '@angular/common/http';

import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { TransactionManagementModule } from './modules/transaction-management/transaction-management.module';
import { authInterceptor } from './interceptors/auth.interceptor';
import { UserModule } from './modules/user/user.module';
import { AuthComponent } from './components/auth/auth.component';
import { LoginComponent } from './components/auth/login/login.component';
import { RegistrationComponent } from './components/auth/registration/registration.component';
import { LoadingComponent } from './components/loading/loading.component';
import { MatTabsModule } from '@angular/material/tabs';

@NgModule({
  declarations: [AppComponent, AuthComponent, LoadingComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    TransactionManagementModule,
    MatDialogModule,
    MatTabsModule,
    UserModule,
    MatIconModule,
    MatButtonModule,
    LoginComponent,
    RegistrationComponent,
  ],
  providers: [
    provideClientHydration(),
    provideAnimationsAsync(),
    provideHttpClient(withInterceptors([authInterceptor])),
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
