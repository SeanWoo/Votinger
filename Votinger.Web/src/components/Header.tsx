import React from 'react';
import { Container, Nav, Navbar } from 'react-bootstrap';
import { NavLink } from 'react-router-dom';

import '../css/mainApp.scss';

const Header: React.FC = () => {
    return (
        <Navbar bg="dark" variant="dark" className="header shadow">
            <Container>
                <Navbar.Brand as={NavLink} to="/">Votinger</Navbar.Brand>
                <Navbar.Toggle aria-controls="responsive-navbar-nav" />
                <Navbar.Collapse id="responsive-navbar-nav">
                    <Nav.Link as={NavLink} to="/signin">SignIn</Nav.Link>
                </Navbar.Collapse>
            </Container>
        </Navbar>
    );
}

export default Header;