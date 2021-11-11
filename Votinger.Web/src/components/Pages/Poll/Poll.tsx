import React from 'react';
import { ButtonGroup, Card, ToggleButton, ToggleButtonGroup } from 'react-bootstrap';
import { PollModel } from '../../../core/models/dto/PollModels';

interface PollProps {
    poll: PollModel
}

const Poll: React.FC<PollProps> = (props: PollProps) => {
    return (
        <Card className="mb-3">
            <Card.Header className="h5">
                {props.poll.title}
            </Card.Header>
            <Card.Body>
                {props.poll.text != null &&
                    <div className="mb-3">{props.poll.text}</div>
                }
                <ButtonGroup className="d-grid gap-2">
                    {props.poll.answerOptions.map((x, i) => 
                        <ToggleButton key={`answer-id-${x.id}`} value={i}>
                            {x.text}
                        </ToggleButton>
                    )}
                </ButtonGroup>
            </Card.Body>
        </Card>
    );
}

export default Poll;