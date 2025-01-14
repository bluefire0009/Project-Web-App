import React from "react"
import { NavLink } from "react-router";
import Api_url from "./Api_url";
import { fetchUserName } from "./getUserName";

class Navbar extends React.Component<{}, { AdminLoggedIn: boolean, UserLoggedIn: boolean, userName: string }> {

    constructor(props: {}) {
        super(props)

        this.state = {
            UserLoggedIn: false,
            AdminLoggedIn: false,
            userName: ""
        }
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
                this.setState({ UserLoggedIn: false })
                this.setState({ AdminLoggedIn: false })
            } else {
                alert("You are not logged in:");
                window.location.href = '/login';
                this.setState({ UserLoggedIn: true })
                this.setState({ AdminLoggedIn: true })

            }
        } catch (error) {
            alert("Error during logout:" + error);
        }
    };

    CheckIfUserLoggedIn = async () => {
        const response = await fetch(`${Api_url}/api/IsUserLoggedIn`, {
            method: 'GET',
            credentials: 'include', // Include cookies for the session
        });
        if (response.ok) {
            this.setState({ UserLoggedIn: true })
        }
        else {
            this.setState({ UserLoggedIn: false })
        }
    }

    CheckIfAdminLoggedIn = async () => {
        const response = await fetch(`${Api_url}/api/IsAdminLoggedIn`, {
            method: 'GET',
            credentials: 'include', // Include cookies for the session
        });
        if (response.ok) {
            this.setState({ AdminLoggedIn: true })
        }
        else {
            this.setState({ AdminLoggedIn: false })
        }
    }



    componentDidMount = async () => {
        this.CheckIfUserLoggedIn();
        this.CheckIfAdminLoggedIn()
        const Name = await fetchUserName();
        this.setState({ userName: Name })
    }

    render(): React.ReactNode {
        return (
            <nav className="navbar">
                <div className="navbar-left">
                    {this.state.UserLoggedIn || this.state.AdminLoggedIn ? (
                        <span>Logged in as {this.state.userName}</span>
                    ) : (
                        <span>Not Logged In</span>
                    )}
                </div>

                {/* Show these links only if the user is logged in */}
                {this.state.UserLoggedIn && (
                    <>
                        <NavLink to="/user">User</NavLink>
                        <NavLink to="/">Homepage</NavLink>
                        <NavLink to="/calendar">Calendar</NavLink>
                    </>
                )}
                {/* Show these links only if the Admin is logged in */}
                {this.state.AdminLoggedIn && (
                    <>
                        <NavLink to="/admin">Admin</NavLink>
                        <NavLink to="/">Homepage</NavLink>
                        <NavLink to="/calendar">Calendar</NavLink>
                    </>
                )}

                {this.state.UserLoggedIn ? (
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