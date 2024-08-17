import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ITransactionService } from './interfaces/transaction.interface';
import { enviroment } from '../../../../../enviroment';
import { Transaction } from '../../../../../models/transaction';
import { OperationResult } from '../../../../../models/operation-result';
import { HttpResponseCode } from '../../../../../models/enums/http-response-code';

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

  public createTransaction(
    transaction: Transaction
  ): Observable<OperationResult> {
    return this.httpClient.post<OperationResult>(
      `${this.baseApiUrl}/Transaction`,
      {
        ...transaction,
      }
    );
  }

  public deleteTransaction(transactionId: number): Observable<OperationResult> {
    return this.httpClient.delete<OperationResult>(
      `${this.baseApiUrl}/Transaction`,
      { params: { transactionId: transactionId } }
    );
  }

  public getTotalTransactionQuantity(): Observable<{
    isSucess: boolean;
    httpResponseCode: HttpResponseCode;
    message?: string;
    data: number;
  }> {
    return this.httpClient.get<{
      isSucess: boolean;
      httpResponseCode: HttpResponseCode;
      message?: string;
      data: number;
    }>(`${this.baseApiUrl}/Transaction/GetUserTransactionQuantity`);
  }

  public updateTransaction(
    transaction: Transaction
  ): Observable<OperationResult> {
    return this.httpClient.patch<OperationResult>(`${this.baseApiUrl}/Transaction`, {
      ...transaction,
    });
  }
}
