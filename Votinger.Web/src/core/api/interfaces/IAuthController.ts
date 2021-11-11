import { ApiErrorResponse } from '../../models/dto/response/ApiErrorResponse';
import { TokensResponse } from '../../models/dto/TokensModel';
import { SignInRequest } from '../../models/dto/request/SignInRequest';
import { SignUpRequest } from '../../models/dto/request/SingUpRequest';

interface IAuthController {
    signIn(model: SignInRequest) : Promise<TokensResponse | ApiErrorResponse>;
    signUp(model: SignUpRequest) : Promise<TokensResponse | ApiErrorResponse>;
    refreshToken(refreshToken: string) : Promise<TokensResponse | ApiErrorResponse>;
}

export default IAuthController;