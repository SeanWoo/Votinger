import * as React from "react";
import { NavLink } from "react-router-dom";
import { Navbar, Container, Nav, Offcanvas } from "react-bootstrap";

import '../css/mainApp.scss';

const Sidebar: React.FC = () => {
    return (
        <Nav variant="pills" className="sidebar bg-dark">
            <Nav.Link as={NavLink} exact to="/">Home</Nav.Link>
            <Nav.Link as={NavLink} exact to="/polls">Polls</Nav.Link>
        </Nav>
    );
}

export default Sidebar;