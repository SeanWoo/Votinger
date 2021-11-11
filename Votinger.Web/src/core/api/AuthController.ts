import IAuthController from './interfaces/IAuthController';
import { AUTH_SERVER_URL } from '../../config';
import { authRequest } from '../request';
import axios from 'axios';
import { TokensResponse } from '../models/dto/TokensModel';
import { SignInRequest } from '../models/dto/request/SignInRequest';
import { SignUpRequest } from '../models/dto/request/SingUpRequest';
import { ApiErrorResponse } from '../models/dto/response/ApiErrorResponse';

const AuthController : IAuthController = {
    signIn: async (model: SignInRequest): Promise<TokensResponse | ApiErrorResponse> => {
        try {
            var response = await authRequest("/SignIn", {
                method: "POST",
                data: model
            });

            if (response.status != 200) {
                return response.data as ApiErrorResponse;
            }
    
            return response.data as TokensResponse;
        }
        catch (error)
        {
            if (axios.isAxiosError(error))
            {
                return {
                    statusCode: 0,
                    message: 'Server is not responding'
                } as ApiErrorResponse
            }
            else
            {
                console.log("Unexcepted error: ", error)
                throw error;
            }
        }
    },
    signUp: async (model: SignUpRequest): Promise<TokensResponse | ApiErrorResponse> => {
        try {
            var response = await authRequest("/SignUp", {
                method: "POST",
                data: model
            });

            if (response.status != 200) {
                return response.data as ApiErrorResponse;
            }
    
            return response.data as TokensResponse;
        }
        catch (error)
        {
            if (axios.isAxiosError(error))
            {
                return {
                    statusCode: 0,
                    message: 'Server is not responding'
                } as ApiErrorResponse
            }
            else
            {
                console.log("Unexcepted error: ", error)
                throw error;
            }
        }
    },
    refreshToken: async (refreshToken: string): Promise<TokensResponse | ApiErrorResponse> => {
        throw new Error('Function not implemented.');
    }
}

export default AuthController;