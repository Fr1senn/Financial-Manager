import { Component } from '@angular/core';
import { Transaction } from '../../models/transaction';
import { title } from 'process';

@Component({
  selector: 'app-transactions',
  templateUrl: './transactions.component.html',
  styleUrl: './transactions.component.css'
})
export class TransactionsComponent {
  public transactions: Transaction[] = [];
}
