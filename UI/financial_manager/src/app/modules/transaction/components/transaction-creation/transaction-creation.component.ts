import { Component, inject, OnInit } from '@angular/core';
import { ITransactionCategoriesFilter } from '../../utilities/interfaces/ITransactionCategoriesFilter';
import { TransactionCategoriesFilterService } from '../../utilities/transaction-categories-filter.service';
import { Category } from '../../../../models/category';
import { TransactionCategoryService } from '../../services/transaction-category.service';
import { Transaction } from '../../../../models/transaction';
import { TransactionService } from '../../services/transaction.service';
import { TransactionCreationFormValidatorService } from '../../utilities/transaction-creation-form-validator.service';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-transaction-creation',
  templateUrl: './transaction-creation.component.html',
  styleUrl: './transaction-creation.component.css',
})
export class TransactionCreationComponent implements OnInit {
  public transactionCategories: Category[] = [];
  public transaction: Transaction = {
    title: '',
    significance: 1,
    transactionType: '',
    expenseDate: undefined,
    createdAt: new Date(),
  };
  public transactionCreationForm: FormGroup | undefined;

  private readonly transactionCategoriesFilter: ITransactionCategoriesFilter =
    inject(TransactionCategoriesFilterService);
  private readonly transactionCategoryService: TransactionCategoryService =
    inject(TransactionCategoryService);
  private readonly transactionService: TransactionService =
    inject(TransactionService);
  private readonly transactionCreationFormValidatorService: TransactionCreationFormValidatorService =
    inject(TransactionCreationFormValidatorService);

  public createTransaction() {
    if (this.transaction.expenseDate === undefined) {
      this.transaction.expenseDate = new Date();
    } else {
      this.transaction.expenseDate = new Date(this.transaction.expenseDate);
    }
    this.transactionService.createTransaction(this.transaction);
    this.dialogRef.close();
  }

  public ngOnInit(): void {
    this.getTransactionCategories();
    this.transactionCreationForm =
      this.transactionCreationFormValidatorService.createForm();
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
      this.transactionCategoryService.getTransactionCategories();
  }
}
