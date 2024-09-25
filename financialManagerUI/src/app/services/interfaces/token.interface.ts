import { TokenResponse } from '../../entities/tokenResponse';

export interface ITokenService {
  getTokens(): TokenResponse;
  saveTokens(tokens: TokenResponse): void;
  clearTokens(): void;
  isAccessTokenExpired(): boolean;
}
