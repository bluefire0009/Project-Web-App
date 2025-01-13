import React from "react"
import { NavLink } from "react-router";
import Api_url from "./Api_url";
import fetchUserName from "./getUserName";

class Navbar extends React.Component<{}, { LoggedIn: boolean, userName: string }> {

    constructor(props: {}) {
        super(props)

        this.state = { LoggedIn: false, userName: "" }
    }

    handleLogout = async () => {
        try {
            // Call the backend logout endpoint
            const response = await fetch(`${Api_url}/api/Logout`, {
                method: 'GET',
                credentials: 'include', // Include cookies for the session
            });

            if (response.ok) {
                // Handle success (for example, redirect to login page or clear session data)
                // alert("Logged out")
                window.location.href = '/login'; // Redirect to login page (or home page, etc.)
                this.setState({ LoggedIn: false })
            } else {
                alert("You are not logged in:");
                window.location.href = '/login';
                this.setState({ LoggedIn: true })

            }
        } catch (error) {
            alert("Error during logout:" + error);
        }
    };

    CheckIfLoggedIn = async () => {
        const response = await fetch(`${Api_url}/api/IsSessionRegisterd`, {
            method: 'GET',
            credentials: 'include', // Include cookies for the session
        });
        if (response.ok) {
            this.setState({ LoggedIn: true })
        }
        else {
            this.setState({ LoggedIn: false })
        }
    }

    componentDidMount = async () => {
        this.CheckIfLoggedIn();
        const Name = await fetchUserName();
        this.setState({ userName: Name })
    }

    render(): React.ReactNode {
        return (
            <nav className="navbar">
                <div className="navbar-left">
                    {this.state.LoggedIn ? (
                        <span>Logged in as {this.state.userName}</span>
                    ) : (
                        <span>Not Logged In</span>
                    )}
                </div>
                {/* <NavLink to="/">Homepage</NavLink>
                <NavLink to="/calendar">Events</NavLink>
                <NavLink to="/calendar">Calendar</NavLink> */}

                {/* Show these links only if the user is logged in */}
                {this.state.LoggedIn && (
                    <>
                        <NavLink to="/user">User</NavLink>
                        {/* <NavLink to="/reviews">Reviews</NavLink> */}
                        <NavLink to="/">Homepage</NavLink>
                        <NavLink to="/calendar">Events</NavLink>
                        <NavLink to="/calendar">Calendar</NavLink>
                    </>
                )}

                {this.state.LoggedIn ? (
                    <button onClick={this.handleLogout}>Log out</button>
                ) : (
                    <NavLink to="/login">
                        <button>Login</button>
                    </NavLink>
                )}
            </nav>
        );
    }
}

export default Navbar