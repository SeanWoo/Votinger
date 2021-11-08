import ApiError from '../../models/ApiError';
import { SignInModel, TokensModel, SignUpModel } from '../../models/AuthModels';

interface IAuthController {
    signIn(model: SignInModel) : Promise<TokensModel | ApiError>;
    signUp(model: SignUpModel) : Promise<TokensModel | ApiError>;
    refreshToken(refreshToken: string) : Promise<TokensModel | ApiError>;
}

export default IAuthController;