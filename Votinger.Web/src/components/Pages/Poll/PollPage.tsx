import axios from 'axios';
import React, { useEffect, useState } from 'react';

import Post from './Poll';
import AuthController from '../../../core/api/AuthController';
import PollController from '../../../core/api/PollController';
import { PollModel } from '../../../core/models/dto/PollModels';
import Poll from './Poll';
import isApiError from '../../../core/utils/checker';

const PollPage: React.FC = () => {
    const [polls, setPolls] = useState<PollModel[]>([]);
    
    useEffect(() => {
        (async () => {
            const response = await PollController.getFew(0, 20, true); 
            if (!isApiError(response))
                setPolls(response);
        })();
    }, [])

    return (
        <div>
            {polls.length == 0 && 
                <blockquote className="blockquote text-center">
                    Посты отсутствуют
                </blockquote>
            }
            {polls.map(poll => 
                <Poll key={`poll-${poll.id}`} poll={poll}/>
            )}
        </div>
    );
}

export default PollPage;