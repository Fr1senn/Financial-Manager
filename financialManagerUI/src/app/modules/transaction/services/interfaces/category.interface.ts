import { WritableSignal } from '@angular/core';
import { Category } from '../../../../entities/models/category';
import { PageRequest } from '../../../../entities/requests/page.request';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../../../entities/shared/apiResponse';
import { CategoryRequest } from '../../../../entities/requests/category.request';

export interface ICategoryService {
  categories: WritableSignal<Category[] | undefined>;

  getCategories(request: PageRequest): Observable<ApiResponse<Category[]>>;
  createCategory(request: CategoryRequest): Observable<ApiResponse<undefined>>;
}
