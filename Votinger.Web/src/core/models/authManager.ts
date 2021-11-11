import { TokensModel } from './dto/AuthModels';
import * as buffer from 'buffer';
import { JwtClaims } from './dto/JwtClaims';
(window as any).Buffer = buffer.Buffer;

const authManager = {

    saveTokens: (tokens : TokensModel) : void => {
        localStorage.setItem("accessToken", tokens.accessToken);   
        localStorage.setItem("refreshToken", tokens.refreshToken);     
    },

    getClaims : () : JwtClaims | null => {
        const accessToken = localStorage.getItem("accessToken");   

        if (accessToken == null)
            return null;
    
        const accessTokenBodyObject = JSON.parse(Buffer.from(accessToken.split('.')[1], "base64").toString('utf-8'))
    
        const jwtClaims : JwtClaims = {
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
