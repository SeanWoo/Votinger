import ApiError from '../models/ApiError';

function isApiError(x: any): x is ApiError {
    const error = x as ApiError;
    return error.statusCode !== undefined && error.message !== undefined
}

export default isApiError;