import { Component, Input } from '@angular/core';
import { Transaction } from '../../../../entities/models/transaction';

@Component({
  selector: 'app-transactions',
  templateUrl: './transactions.component.html',
  styleUrl: './transactions.component.css',
})
export class TransactionsComponent {
  @Input() public transactions: Transaction[] | undefined;
}
