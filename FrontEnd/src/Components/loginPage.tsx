import React from 'react';
import '../Styling/loginpageCss.css';
import CalendarBackground from '../assets/CalendarBackgroundColour.webp'

export const BaseUrl: string = "http://localhost:5097/api/v1/"


export class SignInForm extends React.Component<{}, { Email: string, Password: string, Message: string }> {
    constructor(props: {}) {
        super(props)

        this.state = {
            Email: "",
            Password: "",
            Message: ""
        }
    }

    HandleLogin = async () => {
        // this.setState({ Message: "login" })

        const payload = {
            Email: this.state.Email,
            Password: this.state.Password,
        };

        try {

            const response = await fetch(`${BaseUrl}Login`, {
                method: `POST`,
                headers: {
                    'Content-Type': 'application/json', // Specify JSON content type
                },
                body: JSON.stringify(payload),
            })

            if (response.ok) {
                const data = await response.json();
                this.setState({ Message: data })
            }
            else {
                this.setState({ Message: "fail" })
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
                                value={this.state.Email}
                                onChange={(e) => this.setState({ Email: e.target.value })}
                            />
                            <div>{this.state.Email}</div>
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
                        <button className="sign-in-button" onClick={this.HandleLogin}>
                            Sign In
                        </button>
                        <p className="sign-in-footer">
                            Don't have an account? <a href="#signup" className="sign-up-link">Sign up</a>
                        </p>
                    </form>
                </div>
            </>
        );
    }
}

