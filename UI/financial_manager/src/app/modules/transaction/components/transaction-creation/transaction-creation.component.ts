import { Component, inject, OnInit } from '@angular/core';
import { TransactionCreationFormValidatorService } from '../../utilities/transaction-creation-form-validator.service';
import { FormGroup } from '@angular/forms';
@Component({
  selector: 'app-transaction-creation',
  templateUrl: './transaction-creation.component.html',
  styleUrl: './transaction-creation.component.css',
})
export class TransactionCreationComponent implements OnInit {
  public transactionCreationForm: FormGroup | undefined;
  private readonly transactionCreationFormValidatorService: TransactionCreationFormValidatorService =
    inject(TransactionCreationFormValidatorService);
  public ngOnInit(): void {
    this.transactionCreationForm =
      this.transactionCreationFormValidatorService.createForm();
  }
}
