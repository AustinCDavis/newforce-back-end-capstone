import React, { useState } from 'react';
import { NavLink as RRNavLink } from "react-router-dom";
import { logout } from '../Managers/UserProfileManager';
import { Collapse, Navbar, NavbarToggler, NavbarBrand, Nav, NavItem, NavLink } from 'reactstrap';
import "bootstrap/dist/css/bootstrap.min.css";

export default function Header({ isLoggedIn, setIsLoggedIn }) {
    const [isOpen, setIsOpen] = useState(false);
    const toggle = () => setIsOpen(!isOpen);
    const user = JSON.parse(localStorage.getItem("user"));

    return (
        <div>
            <Navbar color="light" light expand="md">
                <NavbarBrand tag={RRNavLink} to="/"></NavbarBrand>
                <NavbarToggler onClick={toggle} />
                <Collapse isOpen={isOpen} navbar>
                    <Nav className="auto" navbar>
                        { /* When isLoggedIn === true, we will render the Home link */}
                        {isLoggedIn && user.userType.type === "Patient" &&
                            <>
                                <NavItem>
                                    <NavLink tag={RRNavLink} to="/">Home</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={RRNavLink} onClick={() => {
                                        logout()
                                        setIsLoggedIn(false)
                                    }}>Logout</NavLink>
                                </NavItem>
                            </>
                        }
                        {isLoggedIn && user.userType.type === "Provider" &&
                            <>
                                <NavItem>
                                    <NavLink tag={RRNavLink} to="/">Home</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={RRNavLink} to="/Regimens">Regimens</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={RRNavLink} to="/PatientAssignments">Assignments</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={RRNavLink} onClick={() => {
                                        logout()
                                        setIsLoggedIn(false)
                                    }}>Logout</NavLink>
                                </NavItem>
                            </>
                        }
                        {isLoggedIn && user.userType.type === "Admin" &&
                            <>
                                <NavItem>
                                    <NavLink tag={RRNavLink} to="/">Home</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={RRNavLink} onClick={() => {
                                        logout()
                                        setIsLoggedIn(false)
                                    }}>Logout</NavLink>
                                </NavItem>
                            </>
                        }
                    </Nav>
                    <Nav navbar>
                        {!isLoggedIn &&
                            <>
                                <NavItem>
                                    <NavLink tag={RRNavLink} to="/login">Login</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={RRNavLink} to="/register">Register</NavLink>
                                </NavItem>
                            </>
                        }
                    </Nav>
                </Collapse>
            </Navbar>
        </div>
    );
}