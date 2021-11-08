import React from 'react';
import { Route, Switch } from 'react-router';

import '../css/mainApp.scss';
import PostPage from './Pages/Poll/PostPage';
import HomePage from './Pages/Home/HomePage';
import SignInPage from './Pages/SignIn/SignInPage';

const Body: React.FC = () => {
    return (
        <div className="body">
            <Switch>
                <Route exact path="/">
                    <HomePage></HomePage>
                </Route>
                <Route exact path="/polls">
                    <PostPage></PostPage>
                </Route>
                <Route exact path="/signin">
                    <SignInPage></SignInPage>
                </Route>
            </Switch>
        </div>
    );
}

export default Body;