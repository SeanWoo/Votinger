import { pollRequest } from '../request';
import axios from 'axios';
import IPollController from './interfaces/IPollController';
import { PollResponse } from '../models/dto/response/PollResponse';
import { ApiErrorResponse } from '../models/dto/response/ApiErrorResponse';

const PollController : IPollController = {
    getFew: async (from: number, to: number, includeAnswers: boolean = false): Promise<PollResponse[] | ApiErrorResponse> => {
        try {
            var response = await pollRequest("/GetFew", {
                method: "GET",
                params: {
                    from: from,
                    to: to,
                    includeAnswers: includeAnswers
                }
            });

            if (response.status != 200) {
                return response.data as ApiErrorResponse;
            }
    
            return response.data["polls"] as PollResponse[];
        }
        catch (error)
        {
            if (axios.isAxiosError(error))
            {
                return {
                    statusCode: 0,
                    message: 'Server is not responding'
                } as ApiErrorResponse
            }
            else 
            {
                throw error;
            }
        }
    },
}

export default PollController;