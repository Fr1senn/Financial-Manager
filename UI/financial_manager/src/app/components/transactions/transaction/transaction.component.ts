import { Component, Input } from '@angular/core';
import { Transaction } from '../../../models/transaction';

@Component({
  selector: 'app-transaction',
  templateUrl: './transaction.component.html',
  styleUrl: './transaction.component.css',
})
export class TransactionComponent {
  @Input() transaction: Transaction | undefined;
}
