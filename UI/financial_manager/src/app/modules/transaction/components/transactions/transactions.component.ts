import { Component, inject, OnInit } from '@angular/core';
import { Transaction } from '../../../../models/transaction';
import { MatDialog } from '@angular/material/dialog';
import { TransactionCreationComponent } from '../transaction-creation/transaction-creation.component';
import { TransactionService } from '../../services/transaction.service';

@Component({
  selector: 'app-transactions',
  templateUrl: './transactions.component.html',
  styleUrl: './transactions.component.css',
})
export class TransactionsComponent implements OnInit {
  public transactions: Transaction[] = [];

  private readonly dialog: MatDialog = inject(MatDialog);
  private readonly transactionService: TransactionService =
    inject(TransactionService);

  public ngOnInit(): void {
    this.transactions = this.transactionService.getTransactions();
  }

  public createTransaction(): void {
    this.dialog.open(TransactionCreationComponent);
  }
}
