import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AuthModule } from './modules/auth/auth.module';
import { HomeModule } from './modules/home/home.module';
import { LoadingComponent } from './components/loading/loading.component';
import { ILoadingService } from './services/interfaces/loading.interface';
import { LoadingService } from './services/loading.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, AuthModule, HomeModule, LoadingComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  public loadingService: ILoadingService = inject(LoadingService);
}
