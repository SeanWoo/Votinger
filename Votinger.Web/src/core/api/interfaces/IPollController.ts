import ApiError from '../../models/dto/ApiError';
import { PollModel } from '../../models/dto/PollModels';

interface IPollController {
    getFew(from: number, to: number, includeAnswers: boolean) : Promise<PollModel[] | ApiError>;
}

export default IPollController;