import { Observable } from 'rxjs';
import { OperationResult } from '../../models/operation-result';
import { Category } from '../../models/category';
import { HttpResponseCode } from '../../models/enums/http-response-code';

export interface ICategoryService {
  getCategories(
    packSize: number,
    pageNumber: number
  ): Observable<OperationResult<Category>>;

  getTotalCategoryQuantity(): Observable<object>;

  getTotalCategoriesConsumptiion(): Observable<{
    isSuccess: boolean;
    httpResponseCode: HttpResponseCode;
    message: string;
    data: { key: string; value: number };
  }>;

  createCategory(category: Category): Observable<OperationResult>;
}
