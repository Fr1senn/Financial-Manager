import { Component, inject, OnInit } from '@angular/core';
import { Transaction } from '../../../../models/transaction';
import { MatDialog } from '@angular/material/dialog';
import { TransactionCreationComponent } from '../transaction-creation/transaction-creation.component';
import { TransactionService } from '../../services/transaction.service';
import { OperationResult } from '../../../../models/operation-result';
import { PageEvent } from '@angular/material/paginator';
import { HttpResponseCode } from '../../../../models/enums/http-response-code';

@Component({
  selector: 'app-transactions',
  templateUrl: './transactions.component.html',
  styleUrl: './transactions.component.css',
})
export class TransactionsComponent implements OnInit {
  public transactions: Transaction[] = [];
  public userTranactionQuantity: number = 0;
  public packSize: number = 7;

  private readonly dialog: MatDialog = inject(MatDialog);
  private readonly transactionService: TransactionService =
    inject(TransactionService);

  public ngOnInit(): void {
    this.getTransactions(this.packSize, 0);
    this.transactionService
      .getTotalTransactionQuantity()
      .subscribe(
        (response: {
          isSucess: boolean;
          httpResponseCode: HttpResponseCode;
          message?: string;
          data: number;
        }) => {
          this.userTranactionQuantity = response.data;
        }
      );
  }

  public createTransaction(): void {
    const dialogRef = this.dialog.open(TransactionCreationComponent);

    dialogRef.afterClosed().subscribe((data) => {
      this.getTransactions();
    });
  }

  public pageHandler(event: PageEvent) {
    this.getTransactions(this.packSize, event.pageIndex);
  }
  }

  private getTransactions(packSize: number = 10, pageNumber: number = 0): void {
    this.transactionService
      .getTransactions(packSize, pageNumber)
      .subscribe((value: OperationResult<Transaction>) => {
        this.transactions = value.data!;
      });
  }
}
