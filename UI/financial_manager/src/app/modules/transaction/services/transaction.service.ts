import { inject, Injectable } from '@angular/core';
import { Transaction } from '../../../models/transaction';
import { enviroment } from '../../../enviroment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { OperationResult } from '../../../models/operation-result';
import { ITransactionService } from './interfaces/transaction.interface';
import { HttpResponseCode } from '../../../models/enums/http-response-code';

@Injectable({
  providedIn: 'root',
})
export class TransactionService implements ITransactionService {
  private readonly baseApiUrl: string = enviroment.baseApiUrl;
  private readonly httpClient: HttpClient = inject(HttpClient);

  public getTransactions(
    packSize: number = 10,
    pageNumber: number = 0
  ): Observable<OperationResult<Transaction>> {
    return this.httpClient.get<OperationResult<Transaction>>(
      `${this.baseApiUrl}/Transaction`,
      { params: { packSize: packSize, pageNumber: pageNumber } }
    );
  }
  }

  public createTransaction(transaction: Transaction): void {
    transactions.push(transaction);
  }
}
