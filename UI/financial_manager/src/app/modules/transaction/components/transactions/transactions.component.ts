import { Component, inject, OnInit } from '@angular/core';
import { Transaction } from '../../../../models/transaction';

@Component({
  selector: 'app-transactions',
  templateUrl: './transactions.component.html',
  styleUrl: './transactions.component.css',
})
export class TransactionsComponent implements OnInit {
  public transactions: Transaction[] = [];

  private readonly transactionService: TransactionService =
    inject(TransactionService);

  public ngOnInit(): void {
    this.transactions = this.transactionService.getTransactions();
  }

}
