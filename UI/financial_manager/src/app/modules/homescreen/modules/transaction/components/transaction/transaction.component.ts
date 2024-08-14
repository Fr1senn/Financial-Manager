import { Component, inject, Input, Output, EventEmitter } from '@angular/core';
import { ITransactionDateFormatter } from '../../utilities/interfaces/ITransactionDateFormatter';
import { TransactionDateService } from '../../utilities/transaction-date.service';
import { ITransactionService } from '../../services/interfaces/transaction.interface';
import { TransactionService } from '../../services/transaction.service';
import { Transaction } from '../../../../../../models/transaction';

@Component({
  selector: 'app-transaction',
  templateUrl: './transaction.component.html',
  styleUrl: './transaction.component.css',
})
export class TransactionComponent {
  @Input() transaction: Transaction | undefined;
  @Output() deletedTransaction = new EventEmitter();

  public transactionDateService: ITransactionDateFormatter = inject(
    TransactionDateService
  );
  private readonly transactionService: ITransactionService =
    inject(TransactionService);

  public deleteTransaction(): void {
    this.transactionService
      .deleteTransaction(this.transaction?.id!)
      .subscribe(() => {
        this.deletedTransaction.emit(this.transaction);
      });
  }
}