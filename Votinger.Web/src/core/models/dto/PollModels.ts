export type PollModel = {
    id: number,
    userId: number,
    title: string,
    text: string,
    answerOptions: AnswerOptionModel[]
}

export type AnswerOptionModel = {
    id: number,
    text: string,
    numberOfReplies: number,
    pollId: number,
    repliedUsers: RepliedUserModel[]
}

export type RepliedUserModel = {
    id: number,
    pollAnswerOptionId: number,
    userId: number
}

export type CreatePollRequest = {
    title: string;
    answerOptions: string[];
}