import { Component, inject } from '@angular/core';
import { Transaction } from '../../../../models/transaction';

@Component({
  selector: 'app-transactions',
  templateUrl: './transactions.component.html',
  styleUrl: './transactions.component.css',
})
export class TransactionsComponent {
  public transactions: Transaction[] = [];

}
