import ApiError from './ApiError';

type BaseApiResponse = {
    status: "Success" | "Error",
    result: any,
    error: ApiError
}


export default BaseApiResponse;