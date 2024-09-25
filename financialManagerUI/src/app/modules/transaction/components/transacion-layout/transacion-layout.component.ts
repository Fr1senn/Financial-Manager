import { Component, inject } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { TransactionCreationComponent } from '../transaction-creation/transaction-creation.component';

@Component({
  selector: 'app-transacion-layout',
  templateUrl: './transacion-layout.component.html',
  styleUrl: './transacion-layout.component.css',
})
export class TransacionLayoutComponent {
  private readonly transactionCreationDialog: MatDialog = inject(MatDialog);

  public openTransactionCreationDialog(): void {
    const dialogReg = this.transactionCreationDialog.open(
      TransactionCreationComponent
    );
  }
}
