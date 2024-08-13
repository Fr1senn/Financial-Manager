import { Component, inject, OnInit } from '@angular/core';
import { ITransactionCategoriesFilter } from '../../utilities/interfaces/ITransactionCategoriesFilter';
import { TransactionCategoriesFilterService } from '../../utilities/transaction-categories-filter.service';
import { Category } from '../../../../models/category';
import { CategoryService } from '../../services/category.service';
import { Transaction } from '../../../../models/transaction';
import { TransactionService } from '../../services/transaction.service';
import { MatDialogRef } from '@angular/material/dialog';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-transaction-creation',
  templateUrl: './transaction-creation.component.html',
  styleUrl: './transaction-creation.component.css',
})
export class TransactionCreationComponent implements OnInit {
  public transactionTypes = [
    { key: 'income', value: 'Прибуток' },
    { key: 'expense', value: 'Витрата' },
  ];
  public transactionCategories: Category[] = [];
  public selectedTransactionType: string = '';
  public seekingTransactionCategory: string = '';
  public transactionCreationForm: FormGroup | undefined;

  private readonly dialogRef = inject(
    MatDialogRef<TransactionCreationComponent>
  );

  private readonly transactionCategoriesFilter: ITransactionCategoriesFilter =
    inject(TransactionCategoriesFilterService);
  private readonly categoryService: CategoryService = inject(CategoryService);
  private readonly transactionService: TransactionService =
    inject(TransactionService);

  public createTransaction() {
    if (this.transaction.expenseDate === undefined) {
      this.transaction.expenseDate = new Date();
    } else {
      this.transaction.expenseDate = new Date(this.transaction.expenseDate);
    }
    this.transactionService.createTransaction(this.transaction);
    this.dialogRef.close();
    const transaction: Transaction = new Transaction(
      this.transactionCreationForm?.value.title,
      this.transactionCreationForm?.value.significance,
      this.transactionCreationForm?.value.transactionType,
      this.transactionCreationForm?.value.expenseDate,
      new Category('entertaiment')
    );
  }

  public ngOnInit(): void {
    this.getTransactionCategories();
    this.transactionCreationForm = this.createForm();
  }

  public filterTransactionCategories() {
    if (this.seekingTransactionCategory === '') {
      this.getTransactionCategories();
    }
    this.transactionCategories =
      this.transactionCategoriesFilter.filterTransactionCategory(
        this.seekingTransactionCategory,
        this.transactionCategories
      );
  }

  private getTransactionCategories(): void {
    this.transactionCategories =
      this.categoryService.getTransactionCategories();
  }

  private createForm(): FormGroup {
    return new FormGroup({
      title: new FormControl('', [Validators.required]),
      significance: new FormControl(1, [
        Validators.required,
        Validators.min(1),
      ]),
      transactionType: new FormControl('', Validators.required),
      expenseDate: new FormControl(),
    });
  }
}
