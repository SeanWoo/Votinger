import React from 'react';
import { Route, Switch } from 'react-router';

import Home from './Pages/Home';
import Test1 from './Pages/Test1';
import Test2 from './Pages/Test2';

import '../css/mainApp.scss';

const Body: React.FC = () => {
    return (
        <div className="body">
            <Switch>
                <Route exact path="/">
                    <Home></Home>
                </Route>
                <Route exact path="/test1">
                    <Test1></Test1>
                </Route>
                <Route exact path="/test2">
                    <Test2></Test2>
                </Route>
            </Switch>
        </div>
    );
}

export default Body;