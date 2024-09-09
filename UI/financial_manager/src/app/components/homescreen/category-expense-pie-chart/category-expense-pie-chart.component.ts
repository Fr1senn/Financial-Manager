import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { ChartData, Chart, registerables } from 'chart.js';

import { BaseChartDirective } from 'ng2-charts';
import { ICategoryService } from '../../../services/interfaces/category.interface';
import { CategoryService } from '../../../services/category.service';

Chart.register(...registerables);

@Component({
  standalone: true,
  selector: 'app-category-expense-pie-chart',
  imports: [BaseChartDirective, CommonModule],
  templateUrl: './category-expense-pie-chart.component.html',
  styleUrl: './category-expense-pie-chart.component.css',
})
export class CategoryExpensePieChartComponent implements OnInit {
  public chartData: ChartData<'pie', number[], string> = {
    labels: [],
    datasets: [{ data: [0] }],
  };

  private readonly categoryService: ICategoryService = inject(CategoryService);

  public ngOnInit(): void {
    this.getTotalCategoriesConsumption();
  }

  private getTotalCategoriesConsumption(): void {
    this.categoryService.getTotalCategoriesConsumptiion().subscribe((res) => {
      if (res.isSuccess) {
        this.chartData.labels = Object.keys(res.data);
        this.chartData.datasets = [
          { data: Object.values(res.data) as number[] },
        ];
      }
    });
  }
}
