import React, { HtmlHTMLAttributes } from 'react';
import { Button, Form } from 'react-bootstrap';
import { SignInModel, TokensModel } from '../../../core/models/AuthModels';
import AuthController from '../../../core/api/AuthController';
import { useState } from 'react';
import isApiError from '../../../core/utils/checker';

const SignInPage: React.FC = () => {
    const [tokens, setTokens] = useState<TokensModel>()


    const submit = (event: React.SyntheticEvent) => {
        event.preventDefault();

        const form = event.target as typeof event.target & {
            login: { value: string },
            password: { value: string }
        }

        const model : SignInModel = {
            login: form.login.value,
            password: form.password.value
        };

        (async () => {
            const response = await AuthController.signIn(model);
            
            if (isApiError(response))
            {
                console.log(response)
            }
            else
            {
                localStorage.setItem("accessToken", response.accessToken);
                localStorage.setItem("refreshToken", response.refreshToken);
                setTokens(response)
            }
        })();
    }

    return (
        <div>
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
                    Войти
                </Button>
            </Form>
        </div>
    );
}

export default SignInPage;