import { TokensResponse } from '../../core/models/dto/TokensModel';

const PREFIX = "AUTH/";

export const authActionsTypes = {
    UPDATE_AUTH_TOKENS: `${PREFIX}UPDATE_AUTH_TOKENS`,
}

export const authActions = {
    updateTokens: (tokens : TokensResponse) : AUTH_UPDATE_TOKENS => ({
        type: authActionsTypes.UPDATE_AUTH_TOKENS,
        tokens: tokens
    })
}

export type AuthActionTypes = AUTH_UPDATE_TOKENS;

type AUTH_UPDATE_TOKENS = {
    type: typeof authActionsTypes.UPDATE_AUTH_TOKENS,
    tokens: TokensResponse
}