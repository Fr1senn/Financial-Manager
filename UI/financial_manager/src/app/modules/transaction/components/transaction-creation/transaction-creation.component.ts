import { Component, inject, OnInit } from '@angular/core';
import { TransactionCategoryService } from '../../services/transaction-category.service';
import { TransactionCreationFormValidatorService } from '../../utilities/transaction-creation-form-validator.service';
import { FormGroup } from '@angular/forms';
@Component({
  selector: 'app-transaction-creation',
  templateUrl: './transaction-creation.component.html',
  styleUrl: './transaction-creation.component.css',
})
export class TransactionCreationComponent implements OnInit {
  public transactionCategories: Category[] = [];
  public transactionCreationForm: FormGroup | undefined;
  private readonly transactionCategoryService: TransactionCategoryService =
    inject(TransactionCategoryService);
  private readonly transactionCreationFormValidatorService: TransactionCreationFormValidatorService =
    inject(TransactionCreationFormValidatorService);
  public ngOnInit(): void {
    this.getTransactionCategories();
    this.transactionCreationForm =
      this.transactionCreationFormValidatorService.createForm();
  }

  private getTransactionCategories(): void {
    this.transactionCategories =
      this.transactionCategoryService.getTransactionCategories();
  }
}
