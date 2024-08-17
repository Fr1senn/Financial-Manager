import { Component, inject, Input, Output, EventEmitter } from '@angular/core';
import { ITransactionDateFormatter } from '../../utilities/interfaces/ITransactionDateFormatter';
import { TransactionDateService } from '../../utilities/transaction-date.service';
import { ITransactionService } from '../../services/interfaces/transaction.interface';
import { TransactionService } from '../../services/transaction.service';
import { Transaction } from '../../../../../../models/transaction';
import { MatDialog } from '@angular/material/dialog';
import { TransactionEditingComponent } from '../transaction-editing/transaction-editing.component';

@Component({
  selector: 'app-transaction',
  templateUrl: './transaction.component.html',
  styleUrl: './transaction.component.css',
})
export class TransactionComponent {
  @Input() public transaction: Transaction | undefined;
  @Output() public deleteTransactionEvent = new EventEmitter();

  public transactionDateService: ITransactionDateFormatter = inject(
    TransactionDateService
  );

  private tranactionEditingDialog = inject(MatDialog);

  private readonly transactionService: ITransactionService =
    inject(TransactionService);

  public openTransactionEditingDialog() {
    const dialogRef = this.tranactionEditingDialog.open(
      TransactionEditingComponent,
      { data: this.transaction }
    );
  }

  public deleteTransaction(): void {
    this.transactionService
      .deleteTransaction(this.transaction?.id!)
      .subscribe(() => {
        this.deleteTransactionEvent.emit(this.transaction);
      });
  }
}
