import * as buffer from 'buffer';
import { JwtClaimsModel } from './dto/JwtClaims';
import { TokensResponse } from './dto/TokensModel';
import { useDispatch } from 'react-redux';
import { authActions } from '../../store/auth/authActions';
(window as any).Buffer = buffer.Buffer;

const authManager = {
    getTokens: () : TokensResponse => {
        return { 
            accessToken: localStorage.getItem("accessToken"),
            refreshToken: localStorage.getItem("refreshToken")
        }
    },
    saveTokens: (tokens : TokensResponse) : void => {
        if (tokens.accessToken !== null && tokens.refreshToken !== null){
            localStorage.setItem("accessToken", tokens.accessToken);   
            localStorage.setItem("refreshToken", tokens.refreshToken);    
        }
    },

    getClaims : () : JwtClaimsModel | null => {
        const accessToken = localStorage.getItem("accessToken");   

        if (accessToken == null)
            return null;
    
        const accessTokenBodyObject = JSON.parse(Buffer.from(accessToken.split('.')[1], "base64").toString('utf-8'))
    
        const jwtClaims : JwtClaimsModel = {
            userId: accessTokenBodyObject.id,
            username: accessTokenBodyObject["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"]
        }
        return jwtClaims;
    },

    isValidAccessToken: () : boolean => {
        const accessToken = localStorage.getItem("accessToken");   
        const refreshToken = localStorage.getItem("refreshToken");   
    
        if (accessToken == null || refreshToken == null)
            return false;
    
        const accessTokenBody = JSON.parse(Buffer.from(accessToken.split('.')[1], "base64").toString('utf-8'));
    
        const expired : number = accessTokenBody.exp;
        const now = Math.round(Date.now() / 1000);
    
        if (expired - now < 0){
            return false;
        }
    
        return true;
    },

    signOut: () => {
        localStorage.removeItem("accessToken")
        localStorage.removeItem("refreshToken")
    }
}

export default authManager;
