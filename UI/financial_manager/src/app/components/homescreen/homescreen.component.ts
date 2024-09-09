import { Component, inject, OnInit } from '@angular/core';
import { CategoryExpensePieChartComponent } from './category-expense-pie-chart/category-expense-pie-chart.component';
import { MatTabsModule } from '@angular/material/tabs';
import { MonthlyTransactionsBarChartComponent } from './monthly-transactions-bar-chart/monthly-transactions-bar-chart.component';
import { ILoadingService } from '../../services/interfaces/loading.interface';
import { LoadingService } from '../../services/loading.service';
import { IAuthService } from '../../services/interfaces/auth.interface';
import { AuthService } from '../../services/auth.service';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-homescreen',
  imports: [
    CommonModule,
    MatTabsModule,
    CategoryExpensePieChartComponent,
    MonthlyTransactionsBarChartComponent,
  ],
  templateUrl: './homescreen.component.html',
  styleUrl: './homescreen.component.css',
})
export class HomescreenComponent implements OnInit {
  private readonly loadingService: ILoadingService = inject(LoadingService);
  private readonly authService: IAuthService = inject(AuthService);

  public ngOnInit(): void {
    this.loadingService.show();
    if (this.authService.isUserAuthenticated()) {
      this.loadingService.hide();
    }
  }
}
