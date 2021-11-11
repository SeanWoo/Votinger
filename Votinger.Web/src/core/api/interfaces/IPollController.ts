import { PollResponse } from '../../models/dto/response/PollResponse';
import { ApiErrorResponse } from '../../models/dto/response/ApiErrorResponse';

interface IPollController {
    getFew(from: number, to: number, includeAnswers: boolean) : Promise<PollResponse[] | ApiErrorResponse>;
}

export default IPollController;