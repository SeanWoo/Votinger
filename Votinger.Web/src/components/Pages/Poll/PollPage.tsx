import React, { useEffect, useState } from 'react';
import PollController from '../../../core/api/PollController';
import { PollResponse } from '../../../core/models/dto/response/PollResponse';
import Poll from './Poll';
import CreatePoll from './CreatePoll';
import isApiError from '../../../core/utils/checker';
import { RootState } from '../../../store/reducers';
import { connect, ConnectedProps } from 'react-redux';

const PollPage: React.FC<PollPageProps> = (props: PollPageProps) => {
    const [polls, setPolls] = useState<PollResponse[]>([]);
    const [page, setPage] = useState<number>(0)
    
    useEffect(() => {
        (async () => {
            const response = await PollController.getFew(page, page+20, true); 
            if (!isApiError(response)){
                setPolls([...polls, ...response]);
            }
                
        })();
    }, [page])

    const loadingPosts = (event : React.UIEvent<HTMLElement>) => {
        const needDownScroll = event.currentTarget.scrollTop + event.currentTarget.offsetHeight >= event.currentTarget.scrollHeight

        if (needDownScroll){
            setPage(page + 20)
        }
    }
    
    return (
        <div onScroll={loadingPosts} className='scrollable'>
            {props.isAuthorization && 
                <CreatePoll/>
            }
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

const mapStateToProps = (state : RootState) => {
    return {
        isAuthorization: state.auth.isAuthorized
    }
}

const connector = connect(mapStateToProps);

type PollPageProps = ConnectedProps<typeof connector>

export default connector(PollPage);