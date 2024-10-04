import { WritableSignal } from '@angular/core';
import { Category } from '../../../../entities/models/category';
import { PageRequest } from '../../../../entities/requests/page.request';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../../../entities/shared/apiResponse';
import { CategoryRequest } from '../../../../entities/requests/category.request';
import { PaginatedResult } from '../../../../entities/shared/paginatedResult';

export interface ICategoryService {
  categories: WritableSignal<Category[] | undefined>;
  categoriesNumber: WritableSignal<number | undefined>;

  getCategories(
    request: PageRequest
  ): Observable<ApiResponse<PaginatedResult<Category[]>>>;
  createCategory(request: CategoryRequest): Observable<ApiResponse<undefined>>;
}
