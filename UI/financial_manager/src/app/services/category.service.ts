import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { enviroment } from '../enviroment';
import { ICategoryService } from './interfaces/category.interface';
import { Category } from '../models/category';
import { OperationResult } from '../models/operation-result';
import { HttpResponseCode } from '../models/enums/http-response-code';

@Injectable({
  providedIn: 'root',
})
export class CategoryService implements ICategoryService {
  private readonly baseApiUrl: string = enviroment.baseApiUrl;
  private readonly httpClient: HttpClient = inject(HttpClient);

  public getCategories(
    packSize: number,
    pageNumber: number
  ): Observable<OperationResult<Category>> {
    return this.httpClient.get<OperationResult<Category>>(
      `${this.baseApiUrl}/Category`,
      { params: { packSize: packSize, pageNumber: pageNumber } }
    );
  }

  public getTotalCategoryQuantity(): Observable<{
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
    }>(`${this.baseApiUrl}/Category/GetUserCategoryQuantity`);
  }

  public getTotalCategoriesConsumptiion(): Observable<{
    isSuccess: boolean;
    httpResponseCode: HttpResponseCode;
    message: string;
    data: { key: string; value: number };
  }> {
    return this.httpClient.get<{
      isSuccess: boolean;
      httpResponseCode: HttpResponseCode;
      message: string;
      data: { key: string; value: number };
    }>(`${this.baseApiUrl}/Category/GetTotalCategoriesConsumption`);
  }

  public createCategory(category: Category): Observable<OperationResult> {
    return this.httpClient.post<OperationResult>(
      `${this.baseApiUrl}/Category`,
      {
        ...category,
      }
    );
  }
}
