import React from 'react';
import { Route, Switch } from 'react-router';

import '../css/mainApp.scss';
import PollPage from './Pages/Poll/PollPage';
import HomePage from './Pages/Home/HomePage';
import SignInPage from './Pages/SignIn/SignInPage';
import SignUpPage from './Pages/SignUp/SignUpPage';

const Body: React.FC = () => {
    return (
        <div className="body">
            <Switch>
                <Route exact path="/">
                    <HomePage></HomePage>
                </Route>
                <Route exact path="/polls">
                    <PollPage></PollPage>
                </Route>
                <Route exact path="/signin">
                    <SignInPage></SignInPage>
                </Route>
                <Route exact path="/signup">
                    <SignUpPage></SignUpPage>
                </Route>
            </Switch>
        </div>
    );
}

export default Body;