import * as React from "react";
import { NavLink } from "react-router-dom";
import { Navbar, Container, Nav, Offcanvas } from "react-bootstrap";

import '../css/mainApp.scss';
import '../css/main.scss';

const Sidebar: React.FC = () => {
    return (
        <Nav variant="pills" className="sidebar bg-dark">
            <Nav.Link as={NavLink} to="/test1">Home</Nav.Link>
            <Nav.Link as={NavLink} to="/test2">Link</Nav.Link>
        </Nav>
    );
}

export default Sidebar;