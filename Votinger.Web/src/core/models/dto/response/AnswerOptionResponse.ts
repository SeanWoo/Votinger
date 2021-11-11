import { RepliedUserResponse } from "./RepliedUserResponse";


export type AnswerOptionResponse = {
    id: number;
    text: string;
    numberOfReplies: number;
    pollId: number;
    repliedUsers: RepliedUserResponse[];
};
