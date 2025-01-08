import React from "react"
import { NavLink, Router } from "react-router";

class Navbar extends React.Component<{}, {}> {
    
    constructor(props:{}){
        super(props)

        this.state = {}
    }

    render(): React.ReactNode {
        return <nav className="navbar">
            <NavLink to="/">
                Homepage
            </NavLink>
            <NavLink to="/calendar">
                Events
            </NavLink>
            <NavLink to="/calendar">
                Calendar
            </NavLink>
            <NavLink to="/logout">
                Log out
            </NavLink>
        </nav>
    }
}

export default Navbar