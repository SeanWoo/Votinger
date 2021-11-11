import axios, { AxiosInstance, AxiosRequestHeaders } from "axios";
import authManager from '../models/authManager';
import AuthController from '../api/AuthController';
import isApiError from '../utils/checker';
import { useDispatch } from 'react-redux';
import { authActions } from '../../store/auth/authActions';

function registerAuthInterceptors(...axiosRequests: AxiosInstance[]) {
    var a = 1;
    var b = 2;
    var c = a + b;
    axiosRequests.forEach(x => {
        x.interceptors.request.use(
            (request) => {
                const accessToken = localStorage.getItem("accessToken");
                const refreshToken = localStorage.getItem("refreshToken");
                //TODO: сделать проверку по времени и обновление токена
                if (accessToken !== null)
                {
                    if (authManager.isValidAccessToken())
                    {
                        (request.headers as AxiosRequestHeaders).Authorization = "Bearer " + accessToken;
                    }
                    else
                    {
                        (async () => {
                            const tokens = await AuthController.refreshToken(refreshToken as string)

                            if (!isApiError(tokens))
                            {
                                const dispatch = useDispatch()
                                dispatch(authActions.updateTokens(tokens))
                            }
                        })();
                    }
                }

                return request
            }
        )
    })
}

export default registerAuthInterceptors;