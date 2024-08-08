import { Component, inject, Input } from '@angular/core';
import { Transaction } from '../../../../models/transaction';
import { ITransactionDateFormatter } from '../../utilities/interfaces/ITransactionDateFormatter';
import { TransactionDateService } from '../../utilities/transaction-date.service';

@Component({
  selector: 'app-transaction',
  templateUrl: './transaction.component.html',
  styleUrl: './transaction.component.css',
})
export class TransactionComponent {
  @Input() transaction: Transaction | undefined;

  public transactionDateService: ITransactionDateFormatter = inject(TransactionDateService);
}
