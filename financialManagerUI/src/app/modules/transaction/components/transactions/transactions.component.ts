import { Component, inject, OnInit, WritableSignal } from '@angular/core';
import { Transaction } from '../../../../entities/models/transaction';
import { ITransactionService } from '../../services/interfaces/transaction.interface';
import { TransactionService } from '../../services/transaction.service';
import { PageRequest } from '../../../../entities/requests/page.request';

@Component({
  selector: 'app-transactions',
  templateUrl: './transactions.component.html',
  styleUrl: './transactions.component.css',
})
export class TransactionsComponent implements OnInit {
  private readonly _transactionService: ITransactionService =
    inject(TransactionService);

  public transactions: WritableSignal<Transaction[] | undefined> =
    this._transactionService.transactions;

  public ngOnInit(): void {
    this.getTransactions();
  }

  private getTransactions(): void {
    const request: PageRequest = new PageRequest(10, 0);
    this._transactionService.getTransactions(request).subscribe();
  }
}
