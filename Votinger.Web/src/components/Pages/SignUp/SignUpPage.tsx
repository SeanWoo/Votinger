import React from 'react';
import { Button, Form } from 'react-bootstrap';
import AuthController from '../../../core/api/AuthController';
import isApiError from '../../../core/utils/checker';
import { connect, ConnectedProps, useDispatch } from 'react-redux';
import { authActions } from '../../../store/auth/authActions';
import { RootState } from '../../../store/reducers';
import { SignUpRequest } from '../../../core/models/dto/request/SingUpRequest';

const SignUpPage: React.FC<SignUpPageProps> = (props: SignUpPageProps) => {
    const dispatch = useDispatch();

    const submit = (event: React.SyntheticEvent) => {
        event.preventDefault();

        const form = event.target as typeof event.target & {
            login: { value: string },
            password: { value: string }
        }

        const model : SignUpRequest = {
            login: form.login.value,
            password: form.password.value
        };

        (async () => {
            const response = await AuthController.signUp(model);
            
            if (isApiError(response))
            {
                console.log("123")
                console.log(response)
            }
            else
            {
                console.log("456")
                dispatch(authActions.updateTokens(response))
            }
        })();
    }

    return (
        <div>
            {props.isAuthorized ? 
                <blockquote className="blockquote text-center">
                    Вы уже зарегестрированы как 
                </blockquote>
            :
                <Form onSubmit={submit}>
                    <Form.Group className="mb-3">
                        <Form.Label>Введите логин: </Form.Label>
                        <Form.Control name="login" placeholder='MyLogin'/>
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Введите пароль: </Form.Label>
                        <Form.Control name="password" placeholder='password'/>
                    </Form.Group>
                    <Button type="submit">
                        Зарегестрироваться
                    </Button>
                </Form>
            }
        </div>
    );
}

const mapStateToProps = (state : RootState) => {
    return {
        isAuthorized: state.auth.isAuthorized
    }
}

const connector = connect(mapStateToProps)

type SignUpPageProps = ConnectedProps<typeof connector>

export default connector(SignUpPage);