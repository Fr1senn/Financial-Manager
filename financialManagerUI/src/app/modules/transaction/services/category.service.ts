import { inject, Injectable, signal, WritableSignal } from '@angular/core';
import { ICategoryService } from './interfaces/category.interface';
import { Observable, tap } from 'rxjs';
import { Category } from '../../../entities/models/category';
import { PageRequest } from '../../../entities/requests/page.request';
import { ApiResponse } from '../../../entities/shared/apiResponse';
import { API_URL } from '../../../enviroment';
import { HttpClient } from '@angular/common/http';
import { CategoryRequest } from '../../../entities/requests/category.request';
import { PaginatedResult } from '../../../entities/shared/paginatedResult';

@Injectable({
  providedIn: 'root',
})
export class CategoryService implements ICategoryService {
  public categories: WritableSignal<Category[] | undefined> = signal<
    Category[] | undefined
  >(undefined);
  public categoriesNumber: WritableSignal<number | undefined> = signal<
    number | undefined
  >(undefined);

  private readonly API_URL: string = `${API_URL}/category`;
  private readonly _httpClient: HttpClient = inject(HttpClient);

  public getCategories(
    request: PageRequest
  ): Observable<ApiResponse<PaginatedResult<Category[]>>> {
    return this._httpClient
      .post<ApiResponse<PaginatedResult<Category[]>>>(`${this.API_URL}/all`, {
        ...request,
      })
      .pipe(
        tap((res) => {
          if (res.isSuccess && res.result) {
            this.categories.set(res.result.data);
            this.categoriesNumber.set(res.result.totalCount);
          }
        })
      );
  }

  public createCategory(
    request: CategoryRequest
  ): Observable<ApiResponse<undefined>> {
    return this._httpClient
      .post<ApiResponse<undefined>>(`${this.API_URL}/create`, { ...request })
      .pipe(
        tap((res) => {
          if (res.isSuccess) {
            this.getCategories(new PageRequest(5, 0)).subscribe();
          }
        })
      );
  }
}
