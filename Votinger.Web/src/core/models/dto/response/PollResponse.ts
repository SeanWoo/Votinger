import { AnswerOptionResponse } from "./AnswerOptionResponse"

export type PollResponse = {
    id: number,
    userId: number,
    title: string,
    text: string,
    answerOptions: AnswerOptionResponse[]
}


