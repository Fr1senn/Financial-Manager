import { Injectable } from '@angular/core';
import { Transaction } from '../../../models/transaction';


@Injectable({
  providedIn: 'root',
})
export class TransactionService {
  public getTransactions() {
    return transactions;
  }

  public createTransaction(transaction: Transaction): void {
    transactions.push(transaction);
  }
}
