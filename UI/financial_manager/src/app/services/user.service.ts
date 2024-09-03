import { inject, Injectable } from '@angular/core';
import { IUserService } from './interfaces/user.interface';
import { enviroment } from '../enviroment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { OperationResult } from '../models/operation-result';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root',
})
export class UserService implements IUserService {
  private readonly baseApiUrl: string = enviroment.baseApiUrl;
  private readonly httpClient: HttpClient = inject(HttpClient);

  public getCurrentUserCredentials(): Observable<OperationResult<User>> {
    return this.httpClient.get<OperationResult<User>>(
      `${this.baseApiUrl}/User`
    );
  }
}
