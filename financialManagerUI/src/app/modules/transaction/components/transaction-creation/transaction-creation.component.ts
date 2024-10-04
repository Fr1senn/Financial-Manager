import {
  Component,
  inject,
  OnInit,
  signal,
  WritableSignal,
} from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { TransactionRequest } from '../../../../entities/requests/transaction.request';
import { ICategoryService } from '../../services/interfaces/category.interface';
import { CategoryService } from '../../services/category.service';
import { Category } from '../../../../entities/models/category';
import { PageRequest } from '../../../../entities/requests/page.request';
import { CategoryRequest } from '../../../../entities/requests/category.request';
import { ITransactionService } from '../../services/interfaces/transaction.interface';
import { TransactionService } from '../../services/transaction.service';
import { tap } from 'rxjs';
import { CategoryCreationComponent } from '../category-creation/category-creation.component';
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-transaction-creation',
  templateUrl: './transaction-creation.component.html',
})
export class TransactionCreationComponent implements OnInit {
  private readonly transactionCreationDialogRef: MatDialogRef<TransactionCreationComponent> =
    inject(MatDialogRef<TransactionCreationComponent>);
  private readonly categoryCreationDialog: MatDialog = inject(MatDialog);
  private readonly _categoryService: ICategoryService = inject(CategoryService);
  private readonly _transactionService: ITransactionService =
    inject(TransactionService);

  public transactionCreationForm: FormGroup | undefined;
  public transactionTypes: { value: string; displayedValue: string }[] = [
    { value: 'income', displayedValue: 'Дохід' },
    { value: 'expense', displayedValue: 'Витрата' },
  ];
  public categories: WritableSignal<Category[] | undefined> =
    this._categoryService.categories;
  public categoriesNumber: WritableSignal<number | undefined> =
    this._categoryService.categoriesNumber;
  public currentPageSize: WritableSignal<number> = signal<number>(3);

  public ngOnInit(): void {
    this.getCategories(new PageRequest(this.currentPageSize(), 0));
    this.transactionCreationForm = this.createTransactionCreationForm();
  }

  public createTransaction(): void {
    const request: TransactionRequest = new TransactionRequest(
      this.transactionCreationForm?.value.title,
      this.transactionCreationForm?.value.significance,
      this.transactionCreationForm?.value.transactionType,
      this.transactionCreationForm?.value.transactionType === 'expense'
        ? this.transactionCreationForm?.value.expenseDate
        : undefined,
      this.transactionCreationForm?.value.transactionType === 'expense'
        ? new CategoryRequest(this.transactionCreationForm?.value.categoryTitle)
        : undefined
    );

    this._transactionService
      .createTransaction(request)
      .pipe(
        tap((res) => {
          if (res.isSuccess) {
            this.closeDialog();
          }
        })
      )
      .subscribe();
  }

  public closeDialog(): void {
    this.transactionCreationDialogRef.close();
  }

  public openCategoryCreationDialog(): void {
    this.categoryCreationDialog.open(CategoryCreationComponent);
  }

  public pageHandler($event: PageEvent): void {
    this.currentPageSize.set($event.pageSize);
    this.getCategories(new PageRequest($event.pageSize, $event.pageIndex));
  }

  private getCategories(request: PageRequest): void {
    this._categoryService.getCategories(request).subscribe();
  }

  private createTransactionCreationForm(): FormGroup {
    return new FormGroup({
      title: new FormControl('', [
        Validators.required,
        Validators.minLength(5),
      ]),
      significance: new FormControl(1, [
        Validators.required,
        Validators.min(1),
        Validators.max(99999.99),
      ]),
      transactionType: new FormControl('income', [Validators.required]),
      expenseDate: new FormControl(new Date()),
      categoryTitle: new FormControl(''),
    });
  }
}