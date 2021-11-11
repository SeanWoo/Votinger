import { combineReducers, createStore } from "redux";
import authReducer from "./auth/authReducer";
import { IAuthState } from './auth/authReducer';

export interface IRootState {
    auth: IAuthState
}


const reducers = combineReducers({
    auth: authReducer
})


export type RootState = ReturnType<typeof reducers>;

export default reducers