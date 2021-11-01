import React from 'react';
import { Container, Navbar } from 'react-bootstrap';
import { NavLink } from 'react-router-dom';

import '../css/mainApp.scss';

const Header: React.FC = () => {
    return (
        <Navbar bg="dark" variant="dark" className="header shadow">
            <Container>
                <Navbar.Brand as={NavLink} to="/">Votinger</Navbar.Brand>
            </Container>
        </Navbar>
    );
}

export default Header;