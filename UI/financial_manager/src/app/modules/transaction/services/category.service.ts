import { inject, Injectable } from '@angular/core';
import { Category } from '../../../models/category';
import { enviroment } from '../../../enviroment';
import { HttpClient } from '@angular/common/http';
import { ICategoryService } from './interfaces/category.interface';
import { Observable } from 'rxjs';
import { OperationResult } from '../../../models/operation-result';
import { HttpResponseCode } from '../../../models/enums/http-response-code';

@Injectable({
  providedIn: 'root',
})
export class CategoryService implements ICategoryService {
  private readonly baseApiUrl: string = enviroment.baseApiUrl;
  private readonly httpClient: HttpClient = inject(HttpClient);

  getCategories(
    packSize: number,
    pageNumber: number
  ): Observable<OperationResult<Category>> {
    return this.httpClient.get<OperationResult<Category>>(
      `${this.baseApiUrl}/Category`,
      { params: { packSize: packSize, pageNumber: pageNumber } }
    );
  }

  getTotalCategoryQuantity(): Observable<{
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
}
