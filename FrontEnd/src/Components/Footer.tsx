import React from "react"
import { NavLink } from "react-router";

class Footer extends React.Component<{}, {}> {
    
    constructor(props:{}){
        super(props)

        this.state = {}
    }

    render(): React.ReactNode {
        return <footer style={{ textAlign: 'center', marginTop: '50px', fontSize: '14px' }}>
            <NavLink to="/contact" style={{ margin: '0 10px', textDecoration: 'none', color: '#007BFF' }} end>
                Contact
            </NavLink>
            <NavLink to="/contact" style={{ margin: '0 10px', textDecoration: 'none', color: '#007BFF' }} end>
                Information
            </NavLink>
            <NavLink to="/bug" style={{ margin: '0 10px', textDecoration: 'none', color: '#007BFF' }} end>
                Report bug
            </NavLink>
        </footer>
    }
}

export default Footer