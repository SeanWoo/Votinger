import React from 'react';

import Sidebar from './Sidebar';
import Body from './Body'
import Header from './Header'
import { Col, Container, Row } from 'react-bootstrap';

import '../css/mainApp.scss';

const App: React.FC = () => {
    return (
        <div>
            <Header></Header>
            <Container>
                <main className="main">
                    <Sidebar></Sidebar>
                    <Body></Body>
                </main>
            </Container>
        </div>
    );
}

export default App;