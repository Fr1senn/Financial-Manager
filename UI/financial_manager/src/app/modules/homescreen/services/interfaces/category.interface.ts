import { Observable } from 'rxjs';
import { OperationResult } from '../../../../models/operation-result';
import { Category } from '../../../../models/category';

export interface ICategoryService {
  getCategories(
    packSize: number,
    pageNumber: number
  ): Observable<OperationResult<Category>>;

  getTotalCategoryQuantity(): Observable<object>;

  createCategory(category: Category): Observable<OperationResult>;
}
