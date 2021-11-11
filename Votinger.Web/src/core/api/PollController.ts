import ApiError from '../models/dto/ApiError';
import { pollRequest } from '../request';
import axios from 'axios';
import IPollController from './interfaces/IPollController';
import { PollModel } from '../models/dto/PollModels';

const PollController : IPollController = {
    getFew: async (from: number, to: number, includeAnswers: boolean = false): Promise<PollModel[] | ApiError> => {
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
                return response.data as ApiError;
            }
    
            return response.data["polls"] as PollModel[];
        }
        catch (error)
        {
            if (axios.isAxiosError(error))
            {
                return {
                    statusCode: 0,
                    message: 'Server is not responding'
                } as ApiError
            }
            else 
            {
                throw error;
            }
        }
    },
}

export default PollController;