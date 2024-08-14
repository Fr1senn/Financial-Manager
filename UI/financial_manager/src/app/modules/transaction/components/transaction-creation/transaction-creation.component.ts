import { Component, inject, OnInit } from '@angular/core';
import { ITransactionCategoriesFilter } from '../../utilities/interfaces/ITransactionCategoriesFilter';
import { TransactionCategoriesFilterService } from '../../utilities/transaction-categories-filter.service';
import { Category } from '../../../../models/category';
import { CategoryService } from '../../services/category.service';
import { Transaction } from '../../../../models/transaction';
import { TransactionService } from '../../services/transaction.service';
import { MatDialogRef } from '@angular/material/dialog';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { OperationResult } from '../../../../models/operation-result';
import { DateTime } from 'luxon';
import { PageEvent } from '@angular/material/paginator';
import { HttpResponseCode } from '../../../../models/enums/http-response-code';

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
  public selectedTransactionType: string = 'expense';
  public seekingTransactionCategory: string = '';
  public transactionCreationForm: FormGroup | undefined;
  public errorMessage: string = '';
  public userCategoryQuantity: number = 0;

  private readonly dialogRef = inject(
    MatDialogRef<TransactionCreationComponent>
  );

  private readonly transactionCategoriesFilter: ITransactionCategoriesFilter =
    inject(TransactionCategoriesFilterService);
  private readonly categoryService: CategoryService = inject(CategoryService);
  private readonly transactionService: TransactionService =
    inject(TransactionService);

  public createTransaction() {
    const transaction: Transaction = new Transaction(
      this.transactionCreationForm?.value.title,
      this.transactionCreationForm?.value.significance,
      this.transactionCreationForm?.value.transactionType,
      this.transactionCreationForm?.value.transactionType === 'expense' &&
      this.transactionCreationForm?.value.expenseDate === null
        ? DateTime.fromJSDate(new Date()).toISO()
        : this.transactionCreationForm?.value.expenseDate,
      new Category(this.transactionCreationForm?.value.transactionCategory)
    );
    this.transactionService
      .createTransaction(transaction)
      .subscribe((data: OperationResult) => {
        if (!data.isSuccess) {
          this.errorMessage = data.message!;
        } else {
          this.dialogRef.close();
        }
      });
  }

  public ngOnInit(): void {
    this.getCategories(4, 0);
    this.categoryService
      .getTotalCategoryQuantity()
      .subscribe(
        (response: {
          isSucess: boolean;
          httpResponseCode: HttpResponseCode;
          message?: string;
          data: number;
        }) => {
          this.userCategoryQuantity = response.data;
        }
      );
    this.transactionCreationForm = this.createForm();
  }

  public filterTransactionCategories() {
    if (this.seekingTransactionCategory === '') {
      this.getCategories();
    }
    this.transactionCategories =
      this.transactionCategoriesFilter.filterTransactionCategory(
        this.seekingTransactionCategory,
        this.transactionCategories
      );
  }

  public pageHandler(e: PageEvent) {
    this.getCategories(4, e.pageIndex);
  }

  private getCategories(packSize: number = 4, pageNumber: number = 0): void {
    this.categoryService
      .getCategories(packSize, pageNumber)
      .subscribe((response: OperationResult<Category>) => {
        this.transactionCategories = response.data!;
      });
  }

  private createForm(): FormGroup {
    const form = new FormGroup({
      title: new FormControl('', [Validators.required]),
      significance: new FormControl(1, [
        Validators.required,
        Validators.min(1),
      ]),
      transactionType: new FormControl('', Validators.required),
      expenseDate: new FormControl(),
      transactionCategory: new FormControl(),
    });

    return form;
  }
}
