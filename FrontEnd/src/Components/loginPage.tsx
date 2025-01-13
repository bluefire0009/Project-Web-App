import React from 'react';
import '../Styling/loginpageCss.css';
import CalendarBackground from '../assets/CalendarBackgroundColour.webp'
import Api_url from "./Api_url"


export class SignInForm extends React.Component<{}, { Username: string, Password: string, Message: string }> {
    constructor(props: {}) {
        super(props)

        this.state = {
            Username: "",
            Password: "",
            Message: ""
        }
    }

    HandleLogin = async (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault()

        const payload = {
            Username: this.state.Username,
            Password: this.state.Password,
        };


        try {

            const response = await fetch(`${Api_url}/api/Login`, {
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
                window.location.href = "/user";  // Relative URL
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

    render(): JSX.Element {
        return (
            <>
                <img
                    className="sign-in-image" src={CalendarBackground} alt="Calendar Background"
                >
                </img>
                <div className="sign-in-container">
                    <h1 className="sign-in-title">Sign In</h1>
                    <form className="sign-in-form">
                        <div className="form-group">
                            <label htmlFor="email" className="form-label">Email</label>
                            <input
                                type="email"
                                id="email"
                                className="form-input"
                                placeholder="Enter your email"
                                value={this.state.Username}
                                onChange={(e) => this.setState({ Username: e.target.value })}
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
                            />
                            <div className='Message'>{this.state.Message}</div>
                        </div>
                        <button className="sign-in-button" onClick={(e) => this.HandleLogin(e)}>
                            Sign In
                        </button>
                        <p className="sign-in-footer">
                            Don't have an account? <a href="/register" className="sign-up-link">Sign up</a>
                        </p>
                    </form>
                </div>
            </>
        );
    }
}

