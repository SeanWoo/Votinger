import SignInModel from '../../models/SignInModel';
import TokensModel from '../../models/TokensModel';

import ApiError from '../../models/ApiError';

interface IAuthController {
    signIn(model: SignInModel) : TokensModel | ApiError;
}