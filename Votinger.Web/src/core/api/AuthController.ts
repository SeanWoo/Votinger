import ApiError from '../models/ApiError';
import IAuthController from './interfaces/IAuthController';
import { SignInModel, TokensModel, SignUpModel } from '../models/AuthModels';
import { AUTH_SERVER_URL } from '../../config';
import { authRequest } from '../request';

const AuthController : IAuthController = {
    signIn: async (model: SignInModel): Promise<TokensModel | ApiError> => {
        try {
            var response = await authRequest("/SignIn", {
                method: "POST",
                data: model
            });

            if (response.status != 200) {
                return response.data
            }
    
            return response.data;
        }
        catch (error)
        {
            console.log("Unexcepted error: ", error)
            return {
                statusCode: 0,
                message: 'Server is not responding'
            }
        }
    },
    signUp: async (model: SignUpModel): Promise<TokensModel | ApiError> => {
        try {
            var response = await authRequest("/SignUp", {
                method: "POST",
                data: model
            });

            if (response.status != 200) {
                return response.data
            }
    
            return response.data;
        }
        catch (error)
        {
            console.log("Unexcepted error: ", error)
            return {
                statusCode: 0,
                message: 'Server is not responding'
            }
        }
    },
    refreshToken: async (refreshToken: string): Promise<TokensModel | ApiError> => {
        throw new Error('Function not implemented.');
    }
}

export default AuthController;