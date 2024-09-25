export type ApiResponse<T> = {
  isSuccess: boolean;
  statusCode: number;
  message: string;
  result?: T;
};
