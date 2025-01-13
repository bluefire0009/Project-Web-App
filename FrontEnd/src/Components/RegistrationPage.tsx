// import React from 'react';
import React from 'react';
import '../Styling/loginpageCss.css';
import CalendarBackground from '../assets/CalendarBackgroundColour.webp'
import Api_url from './Api_url';

export class RegistrationForm extends React.Component<{}, {
    FullName: string;
    Email: string;
    Password: string;
    ConfirmPassword: string;
    Message: string;
}> {
    constructor(props: {}) {
        super(props)
        this.state = {
            FullName: '',
            Email: '',
            Password: '',
            ConfirmPassword: '',
            Message: '',
        }
    }

    HandlesignUp = async (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault()

        const payload = {
            FullName: this.state.FullName,
            Email: this.state.Email,
            Password: this.state.Password,
        };

        if (this.state.Password != this.state.ConfirmPassword) {
            this.setState({ Message: "Passwords don't match" });
            return;
        }

        try {

            const response = await fetch(`${Api_url}/api/User/Register`, {
                method: `POST`,
                headers: {
                    'Content-Type': 'application/json', // Specify JSON content type
                },
                body: JSON.stringify(payload),
                credentials: 'include'
            })

            if (response.ok) {
                const data = await response.text();
                this.setState({ Message: data })
                this.LoginAutomatically(payload.Email, payload.Password)
                // window.location.href = "/user";  // Relative URL
            }
            else {
                const errorText = await response.text();
                const errorMessage = `Error: ${response.status} - ${response.statusText} - ${errorText}`;
                this.setState({ Message: errorMessage });
            }
        }
        catch {
            this.setState({ Message: "Server Error" })
        }
    }

    LoginAutomatically = async (username: string, password: string) => {
        const payload = {
            Username: username,
            Password: password,
        };
        const response = await fetch(`${Api_url}/api/Login`, {
            method: `POST`,
            headers: {
                'Content-Type': 'application/json', // Specify JSON content type
            },
            body: JSON.stringify(payload),
            credentials: 'include'
        })
        // window.location.href = "/user";  // Relative URL
        if (response.ok) { alert("logged in after registering") }
        else {
            const errorText = await response.text();
            const errorMessage = `Error: ${response.status} - ${response.statusText} - ${errorText}`;
            alert("login failed" + errorMessage)
        }
    }


    render(): JSX.Element {
        return (
            <>
                <img
                    className="sign-in-image" src={CalendarBackground} alt="Calendar Background"
                />
                <div className="sign-in-container">
                    <h1 className="sign-in-title">Sign Up</h1>
                    <form className="sign-in-form">
                        <div className="form-group">
                            <label htmlFor="name" className="form-label">Full Name</label>
                            <input
                                type="text"
                                id="name"
                                className="form-input"
                                placeholder="Enter your full name"
                                value={this.state.FullName}
                                onChange={(e) => this.setState({ FullName: e.target.value })}
                                required
                            />
                        </div>
                        <div className="form-group">
                            <label htmlFor="email" className="form-label">Email</label>
                            <input
                                type="email"
                                id="email"
                                className="form-input"
                                placeholder="Enter your email"
                                value={this.state.Email}
                                onChange={(e) => this.setState({ Email: e.target.value })}
                                required
                            />
                        </div>
                        <div className="form-group">
                            <label htmlFor="password" className="form-label">Password</label>
                            <input
                                type="password"
                                id="password"
                                className="form-input"
                                placeholder="Enter your password"
                                value={this.state.Password}
                                onChange={(e) => this.setState({ Password: e.target.value })}
                                required
                            />
                        </div>
                        <div className="form-group">
                            <label htmlFor="confirm-password" className="form-label">Confirm Password</label>
                            <input
                                type="password"
                                id="confirm-password"
                                className="form-input"
                                placeholder="Confirm your password"
                                value={this.state.ConfirmPassword}
                                onChange={(e) => this.setState({ ConfirmPassword: e.target.value })}
                                required
                            />
                        </div>
                        <div className="Message">{this.state.Message}</div>
                        <button type="submit" className="sign-in-button" onClick={(e) => this.HandlesignUp(e)}>
                            Sign Up
                        </button>
                        <p className="sign-in-footer">
                            Already have an account? <a href="/login" className="sign-up-link">Sign In</a>
                        </p>
                    </form>
                </div>
            </>
        );
    }
}

export default RegistrationForm;
