// import React from 'react';
import React from 'react';
import '../Styling/loginpageCss.css';
import CalendarBackground from '../assets/CalendarBackgroundColour.webp'

export class RegistrationForm extends React.Component<{}, {}> {
    constructor(props: {}) {
        super(props)
        this.state = {}
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
                            />
                        </div>
                        <div className="form-group">
                            <label htmlFor="email" className="form-label">Email</label>
                            <input
                                type="email"
                                id="email"
                                className="form-input"
                                placeholder="Enter your email"
                            />
                        </div>
                        <div className="form-group">
                            <label htmlFor="password" className="form-label">Password</label>
                            <input
                                type="password"
                                id="password"
                                className="form-input"
                                placeholder="Enter your password"
                            />
                        </div>
                        <div className="form-group">
                            <label htmlFor="confirm-password" className="form-label">Confirm Password</label>
                            <input
                                type="password"
                                id="confirm-password"
                                className="form-input"
                                placeholder="Confirm your password"
                            />
                        </div>
                        <button type="submit" className="sign-in-button">
                            Sign Up
                        </button>
                        <p className="sign-in-footer">
                            Already have an account? <a href="#signin" className="sign-up-link">Sign In</a>
                        </p>
                    </form>
                </div>
            </>
        );
    }
}

export default RegistrationForm;
