import { Reducer } from 'redux';
import { AuthActionTypes, authActionsTypes } from './authActions';
import authManager from '../../core/models/authManager';
import { JwtClaimsModel } from '../../core/models/dto/JwtClaims';
import { TokensResponse } from '../../core/models/dto/TokensModel';

export interface IAuthState {
    isAuthorized: boolean,
    tokens : TokensResponse
    jwt : JwtClaimsModel
}

const initialState : IAuthState = {
    isAuthorized: authManager.isValidAccessToken(),
    tokens: authManager.getTokens(),
    jwt: {
        userId: 0,
        username: ''
    }
}

const authReducer : Reducer<IAuthState, AuthActionTypes> = (state : IAuthState = initialState, action : AuthActionTypes) => {
    switch(action.type){
        case authActionsTypes.UPDATE_AUTH_TOKENS:
            authManager.saveTokens(action.tokens)
            return {...state, 
                isAuthorized: authManager.isValidAccessToken(),
                tokens: {
                    accessToken: action.tokens.accessToken, 
                    refreshToken: action.tokens.refreshToken
                }}
        default:
            return state
    }
}

export default authReducer;