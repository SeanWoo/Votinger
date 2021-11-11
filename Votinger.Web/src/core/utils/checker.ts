import { ApiErrorResponse } from '../models/dto/response/ApiErrorResponse';

function isApiError(x: any): x is ApiErrorResponse {
    const error = x as ApiErrorResponse;
    return error.statusCode !== undefined && error.message !== undefined
}

export default isApiError;