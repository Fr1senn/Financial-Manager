import {
  Component,
  inject,
  OnInit,
  signal,
  WritableSignal,
} from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Transaction } from '../../../../entities/models/transaction';
import { ITransactionService } from '../../services/interfaces/transaction.interface';
import { TransactionService } from '../../services/transaction.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ICategoryService } from '../../services/interfaces/category.interface';
import { CategoryService } from '../../services/category.service';
import { Category } from '../../../../entities/models/category';
import { PageRequest } from '../../../../entities/requests/page.request';
import { PageEvent } from '@angular/material/paginator';
import { TransactionRequest } from '../../../../entities/requests/transaction.request';
import { CategoryRequest } from '../../../../entities/requests/category.request';
import { tap } from 'rxjs';

@Component({
  selector: 'app-transaction-editing',
  templateUrl: './transaction-editing.component.html',
})
export class TransactionEditingComponent implements OnInit {
  private readonly dialogRef = inject(
    MatDialogRef<TransactionEditingComponent>
  );
  private readonly data = inject<{ transactionId: number }>(MAT_DIALOG_DATA);
  private readonly _transactionService: ITransactionService =
    inject(TransactionService);
  private readonly _categoryService: ICategoryService = inject(CategoryService);

  public transaction: Transaction | undefined;
  public transactionEditingForm: FormGroup | undefined;
  public categories: WritableSignal<Category[] | undefined> =
    this._categoryService.categories;
  public categoriesNumber: WritableSignal<number | undefined> =
    this._categoryService.categoriesNumber;
  public currentPageSize: WritableSignal<number> = signal<number>(3);

  public ngOnInit(): void {
    this.getTransaction();
    this.getCategories(new PageRequest(this.currentPageSize(), 0));
  }

  public closeTransactionEditingDialog() {
    this.dialogRef.close();
  }

  public pageHandler($event: PageEvent): void {
    this.currentPageSize.set($event.pageSize);
    this.getCategories(new PageRequest($event.pageSize, $event.pageIndex));
  }

  public updateTransaction(): void {
    const request: TransactionRequest = new TransactionRequest(
      this.transactionEditingForm?.value.title,
      this.transactionEditingForm?.value.significance,
      this.transaction?.transactionType!,
      this.transaction?.transactionType! === 'expense'
        ? this.transactionEditingForm?.value.expenseDate
        : undefined,
      this.transaction?.transactionType! === 'expense'
        ? new CategoryRequest(
            this.transactionEditingForm?.value.categoryTitle,
            this.transaction?.category?.id
          )
        : undefined,
      this.transaction?.id
    );

    this._transactionService.updateTransaction(request).subscribe((res) => {
      if (res.isSuccess) {
        this.closeTransactionEditingDialog();
      }
    });
  }

  private getTransaction(): void {
    this.transaction = this._transactionService.getTransactionById(
      this.data.transactionId
    );
    this.transactionEditingForm = this.createTransactionEditingForm();
  }

  private getCategories(request: PageRequest): void {
    this._categoryService.getCategories(request).subscribe();
  }

  private createTransactionEditingForm(): FormGroup {
    if (this.transaction?.transactionType === 'income') {
      return new FormGroup({
        title: new FormControl(this.transaction.title, [
          Validators.required,
          Validators.minLength(5),
        ]),
        significance: new FormControl(this.transaction.significance, [
          Validators.required,
          Validators.min(1),
          Validators.max(99999.99),
        ]),
        transactionType: new FormControl({
          value: this.transaction.transactionType,
          disabled: true,
        }),
      });
    } else {
      return new FormGroup({
        title: new FormControl(this.transaction?.title, [
          Validators.required,
          Validators.minLength(5),
        ]),
        significance: new FormControl(this.transaction?.significance, [
          Validators.required,
          Validators.min(1),
          Validators.max(99999.99),
        ]),
        transactionType: new FormControl({
          value: this.transaction?.transactionType,
          disabled: true,
        }),
        expenseDate: new FormControl(new Date(this.transaction?.expenseDate!)),
        categoryTitle: new FormControl(this.transaction?.category?.title),
      });
    }
  }
}
