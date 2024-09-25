import { inject, Injectable, signal, WritableSignal } from '@angular/core';
import { ICategoryService } from './interfaces/category.interface';
import { Observable, tap } from 'rxjs';
import { Category } from '../../../entities/models/category';
import { PageRequest } from '../../../entities/requests/page.request';
import { ApiResponse } from '../../../entities/shared/apiResponse';
import { API_URL } from '../../../enviroment';
import { HttpClient } from '@angular/common/http';
import { CategoryRequest } from '../../../entities/requests/category.request';

@Injectable({
  providedIn: 'root',
})
export class CategoryService implements ICategoryService {
  public categories: WritableSignal<Category[] | undefined> = signal<
    Category[] | undefined
  >(undefined);

  private readonly API_URL: string = `${API_URL}/Category`;
  private readonly _httpClient: HttpClient = inject(HttpClient);

  public getCategories(
    request: PageRequest
  ): Observable<ApiResponse<Category[]>> {
    return this._httpClient
      .get<ApiResponse<Category[]>>(this.API_URL, {
        params: { ...request },
      })
      .pipe(
        tap((res) => {
          if (res.isSuccess && res.result) {
            this.categories.set(res.result);
          }
        })
      );
  }

  public createCategory(
    request: CategoryRequest
  ): Observable<ApiResponse<undefined>> {
    return this._httpClient
      .post<ApiResponse<undefined>>(this.API_URL, { ...request })
      .pipe(
        tap((res) => {
          if (res.isSuccess) {
            this.getCategories(new PageRequest(5, 0)).subscribe();
          }
        })
      );
  }
}
