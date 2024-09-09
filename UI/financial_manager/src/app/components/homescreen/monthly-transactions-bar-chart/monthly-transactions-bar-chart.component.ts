import { Component, inject, OnInit } from '@angular/core';
import { ChartData } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { ITransactionService } from '../../../services/interfaces/transaction.interface';
import { TransactionService } from '../../../services/transaction.service';

@Component({
  standalone: true,
  imports: [BaseChartDirective],
  selector: 'app-monthly-transactions-bar-chart',
  templateUrl: './monthly-transactions-bar-chart.component.html',
  styleUrl: './monthly-transactions-bar-chart.component.css',
})
export class MonthlyTransactionsBarChartComponent implements OnInit {
  public chartData: ChartData<'bar', number[], string> = {
    labels: [],
    datasets: [
      { data: [], label: 'Income' },
      { data: [], label: 'Expense' },
    ],
  };

  private readonly transactionService: ITransactionService =
    inject(TransactionService);

  public ngOnInit(): void {
    this.getMonthlyTransactions(2024);
  }

  private getMonthlyTransactions(year: number): void {
    this.transactionService.getMonthlyTransactions(year).subscribe((res) => {
      if (res.isSuccess) {
        this.chartData.labels = Object.keys(res.data);
        Object.keys(res.data).forEach((month) => {
          const income = res.data[month].income;
          const expense = res.data[month].expense;

          this.chartData.datasets[0].data.push(income);
          this.chartData.datasets[1].data.push(expense);
        });
      }
    });
  }
}
