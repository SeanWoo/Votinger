import React, { useEffect, useState } from 'react';
import { Container, Nav, Navbar } from 'react-bootstrap';
import { connect, ConnectedProps, useDispatch } from 'react-redux';
import { NavLink } from 'react-router-dom';
import { createFalse } from 'typescript';
import authManager from '../core/models/authManager';

import '../css/mainApp.scss';
import { useTypedSelector } from '../store/useTypedSelector';
import { authActions } from '../store/auth/authActions';
import { RootState } from '../store/reducers';

const Header: React.FC<HeaderProps> = (props: HeaderProps) => {
    const dispatch = useDispatch();

    const signOutHandle = (e: React.MouseEvent<HTMLElement>) => {
        authManager.signOut();
        dispatch(authActions.updateTokens({
            accessToken: null,
            refreshToken: null
        }))
    }

    return (
        <Navbar bg="dark" variant="dark" className="header shadow">
            <Container>
                <Navbar.Brand as={NavLink} to="/">Votinger</Navbar.Brand>
                <Navbar.Toggle aria-controls="responsive-navbar-nav" />
                <Navbar.Collapse id="responsive-navbar-nav">
                    {props.isAuthorized ?
                        <div className='d-flex ms-auto'>
                            <Navbar.Text>Вы вошли как {authManager.getClaims()?.username}</Navbar.Text>
                            <Nav.Link onClick={signOutHandle}>Sign out</Nav.Link>
                        </div>
                    : 
                        <div className='d-flex ms-auto'>
                            <Nav.Link as={NavLink} to="/signin">SignIn</Nav.Link>
                            <Nav.Link as={NavLink} to="/signup">SignUp</Nav.Link>
                        </div>
                    }
                </Navbar.Collapse>
            </Container>
        </Navbar>
    );
}

const mapStateToProps = (state : RootState) => {
    return {
        isAuthorized: state.auth.isAuthorized
    }
}

const connector = connect(mapStateToProps);

type HeaderProps = ConnectedProps<typeof connector>

export default connector(Header);