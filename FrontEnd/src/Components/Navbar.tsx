import React from "react"
import { NavLink, Router } from "react-router";

class Navbar extends React.Component<{}, {}> {
    
    constructor(props:{}){
        super(props)

        this.state = {}
    }

    render(): React.ReactNode {
        return <nav className="navbar">
            <NavLink to="/user">
                Homepage
            </NavLink>
            <NavLink to="/day">
                Events
            </NavLink>
            <NavLink to="/user">
                Calendar
            </NavLink>
            <NavLink to="/user">
                Log out
            </NavLink>
        </nav>
    }
}

export default Navbar