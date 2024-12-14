import React from 'react';
import '../Styling/loginpageCss.css';
import CalendarBackground from '../assets/CalendarBackgroundColour.webp'



function SignInForm() {
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
                    <button type="submit" className="sign-in-button">
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

export default SignInForm;
