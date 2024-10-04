import {
  Component,
  inject,
  OnInit,
  signal,
  WritableSignal,
} from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { TransactionCreationComponent } from '../transaction-creation/transaction-creation.component';
import { ITransactionService } from '../../services/interfaces/transaction.interface';
import { TransactionService } from '../../services/transaction.service';
import { Transaction } from '../../../../entities/models/transaction';
import { PageRequest } from '../../../../entities/requests/page.request';
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-transacion-layout',
  templateUrl: './transacion-layout.component.html',
  styleUrl: './transacion-layout.component.css',
})
export class TransacionLayoutComponent implements OnInit {
  private readonly transactionCreationDialog: MatDialog = inject(MatDialog);
  private readonly _transactionService: ITransactionService =
    inject(TransactionService);

  public pageSizeOptions: number[] = [5, 10, 20];
  public transactions: WritableSignal<Transaction[] | undefined> =
    this._transactionService.transactions;
  public transactionsNumber: WritableSignal<number | undefined> =
    this._transactionService.transactionsNumber;
  public currentPageSize: WritableSignal<number> = signal<number>(5);

  public ngOnInit(): void {
    this.getTransactions(new PageRequest(this.currentPageSize(), 0));
  }

  public openTransactionCreationDialog(): void {
    const dialogReg = this.transactionCreationDialog.open(
      TransactionCreationComponent
    );
  }

  public pageHandler($event: PageEvent): void {
    this.currentPageSize.set($event.pageSize);
    this.getTransactions(new PageRequest($event.pageSize, $event.pageIndex));
  }

  private getTransactions(request: PageRequest): void {
    this._transactionService.getTransactions(request).subscribe();
  }
}
