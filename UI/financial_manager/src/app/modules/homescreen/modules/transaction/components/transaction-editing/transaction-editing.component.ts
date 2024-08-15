import { Component, inject, OnInit } from '@angular/core';
import { Transaction } from '../../../../../../models/transaction';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CategoryService } from '../../../../services/category.service';
import { Category } from '../../../../../../models/category';
import { OperationResult } from '../../../../../../models/operation-result';
import { PageEvent } from '@angular/material/paginator';
import { HttpResponseCode } from '../../../../../../models/enums/http-response-code';
import { TransactionService } from '../../services/transaction.service';
import { DateTime } from 'luxon';

@Component({
  selector: 'app-transaction-editing',
  templateUrl: './transaction-editing.component.html',
  styleUrl: './transaction-editing.component.css',
})
export class TransactionEditingComponent implements OnInit {
  public transaction: Transaction = inject<Transaction>(MAT_DIALOG_DATA);
  public transactionEditingForm: FormGroup | undefined;
  public transactionTypes = [
    { key: 'income', value: 'Прибуток' },
    { key: 'expense', value: 'Витрата' },
  ];
  public categories: Category[] | undefined;
  public userCategoryQuantity: number = 0;

  private dialogRef = inject(MatDialogRef<TransactionEditingComponent>);

  private readonly categoryService: CategoryService = inject(CategoryService);
  private readonly transactionService: TransactionService =
    inject(TransactionService);

  public ngOnInit(): void {
    this.transactionEditingForm = this.createTransactionEditingForm();
    this.getCategories();
    this.getUserCategoryQuantity();
  }

  public pageHandler(e: PageEvent): void {
    this.getCategories(4, e.pageIndex);
  }

  public updateTransaction(): void {
    this.transaction.title = this.transactionEditingForm?.value.title;
    this.transaction.significance =
      this.transactionEditingForm?.value.significance;
    this.transaction.expenseDate =
      this.transactionEditingForm?.value.transactionType === 'expense' &&
      this.transactionEditingForm?.value.expenseDate === null
        ? DateTime.fromJSDate(new Date()).toISO()
        : this.transactionEditingForm?.value.expenseDate;
    this.transaction.category = new Category(
      this.transactionEditingForm?.value.transactionCategory
    );
    console.log(this.transaction.transactionType);
    this.transactionService
      .updateTransaction(this.transaction)
      .subscribe((response: OperationResult) => {
        if (response.isSuccess) {
          this.dialogRef.close();
        }
      });
  }

  private getCategories(packSize: number = 4, pageNumber: number = 0): void {
    this.categoryService
      .getCategories(packSize, pageNumber)
      .subscribe((response: OperationResult<Category>) => {
        this.categories = response.data!;
      });
  }

  private getUserCategoryQuantity(): void {
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
  }

  private createTransactionEditingForm(): FormGroup {
    if (this.transaction.transactionType === 'income') {
      return new FormGroup({
        title: new FormControl(this.transaction.title, Validators.required),
        significance: new FormControl(this.transaction.significance, [
          Validators.required,
          Validators.min(1),
        ]),
        transactionType: new FormControl({
          value: this.transaction.transactionType,
          disabled: true,
        }),
      });
    }

    return new FormGroup({
      title: new FormControl(this.transaction.title, Validators.required),
      significance: new FormControl(this.transaction.significance, [
        Validators.required,
        Validators.min(1),
      ]),
      transactionType: new FormControl({
        value: this.transaction.transactionType,
        disabled: true,
      }),
      expenseDate: new FormControl(this.transaction.expenseDate),
      transactionCategory: new FormControl(
        this.transaction.category?.title,
        Validators.required
      ),
    });
  }
}
