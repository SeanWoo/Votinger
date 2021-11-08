import axios from "axios";
import { AUTH_SERVER_URL, POLL_SERVER_URL } from '../config';

export const authRequest = axios.create({
    baseURL: AUTH_SERVER_URL,
    timeout: 5000,
    validateStatus: (err: number) => err < 500,
})

export const pollRequest = axios.create({
    baseURL: POLL_SERVER_URL,
    timeout: 5000,
    validateStatus: (err: number) => err < 500,
})