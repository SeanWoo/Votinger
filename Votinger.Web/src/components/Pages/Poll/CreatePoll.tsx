import React, { useState } from 'react';
import { Form, Button } from 'react-bootstrap';
import { PollResponse } from '../../../core/models/dto/response/PollResponse';
import { AnswerOptionResponse } from "../../../core/models/dto/response/AnswerOptionResponse";
import PollController from '../../../core/api/PollController';

const CreatePoll: React.FC = () => {
    const [answerOptions, setAnswerOptions] = useState<AnswerOptionResponse[]>([])

    const submitHandle = (event : React.SyntheticEvent) => {
        event.preventDefault()

        const form = event.target as typeof event.target & {
            title: { value: string },
            text: { value: string }
        }

        
    }

    return (
        <div className="mb-4 bg-secondary-dark p-3 rounded">
            <Form onSubmit={submitHandle}>
                <Form.Group className="mb-3">
                    <Form.Label>Впишите название голосование</Form.Label>
                    <Form.Control/>
                </Form.Group>
                <Form.Group className="mb-3">
                    <Form.Label>Впишите описание</Form.Label>
                    <Form.Control as="textarea" rows={3} />
                </Form.Group>
                <Form.Group className="mb-3">
                    <Button type="submit">Опубликовать</Button>
                </Form.Group>
            </Form>
        </div>
    );
}

export default CreatePoll;