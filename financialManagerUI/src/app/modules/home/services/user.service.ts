import { inject, Injectable, signal, WritableSignal } from '@angular/core';
import { IUserService } from './interfaces/user.interface';
import { Observable, tap } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { User } from '../../../entities/models/user';
import { API_URL } from '../../../enviroment';
import { ApiResponse } from '../../../entities/shared/apiResponse';
import { UserRequest } from '../../../entities/requests/user.request';

@Injectable({
  providedIn: 'root',
})
export class UserService implements IUserService {
  public userCredentials: WritableSignal<User | null> = signal<User | null>(
    null
  );

  private readonly API_URL: string = `${API_URL}/User`;
  private readonly _httpClient: HttpClient = inject(HttpClient);

  public getUserCredentials(): Observable<ApiResponse<User>> {
    return this._httpClient.get<ApiResponse<User>>(`${this.API_URL}`).pipe(
      tap((res) => {
        if (res.isSuccess && res.result) {
          this.userCredentials.set(res.result);
        }
      })
    );
  }

  public updateUserCredentials(
    request: UserRequest
  ): Observable<ApiResponse<undefined>> {
    return this._httpClient
      .patch<ApiResponse<undefined>>(`${this.API_URL}`, {
        ...request,
      })
      .pipe(
        tap((res) => {
          if (res.isSuccess) {
            const currentUserCredentials = this.userCredentials();
            this.userCredentials.set({
              ...currentUserCredentials,
              ...request,
              registrationDate: currentUserCredentials?.registrationDate!,
            });
          }
        })
      );
  }
}
