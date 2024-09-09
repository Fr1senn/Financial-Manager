import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ITransactionService } from './interfaces/transaction.interface';
import { enviroment } from '../enviroment';
import { Transaction } from '../models/transaction';
import { OperationResult } from '../models/operation-result';
import { HttpResponseCode } from '../models/enums/http-response-code';

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

  public getTotalTransactionQuantity(): Observable<{
    isSuccess: boolean;
    httpResponseCode: HttpResponseCode;
    message?: string;
    data: number;
  }> {
    return this.httpClient.get<{
      isSuccess: boolean;
      httpResponseCode: HttpResponseCode;
      message?: string;
      data: number;
    }>(`${this.baseApiUrl}/Transaction/GetUserTransactionQuantity`);
  }

  public getMonthlyTransactions(year: number): Observable<{
    isSuccess: boolean;
    httpResponseCode: HttpResponseCode;
    message?: string;
    data: { [month: string]: { income: number; expense: number } };
  }> {
    return this.httpClient.get<{
      isSuccess: boolean;
      httpResponseCode: HttpResponseCode;
      message?: string;
      data: { [month: string]: { income: number; expense: number } };
    }>(`${this.baseApiUrl}/Transaction/GetMonthlyTransactions`, {
      params: { year: year },
    });
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

  public updateTransaction(
    transaction: Transaction
  ): Observable<OperationResult> {
    return this.httpClient.patch<OperationResult>(
      `${this.baseApiUrl}/Transaction`,
      {
        ...transaction,
      }
    );
  }
}
