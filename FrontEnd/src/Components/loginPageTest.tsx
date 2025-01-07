import React, { useState } from 'react';
import '../Styling/loginpageCss.css';
import CalendarBackground from '../assets/CalendarBackgroundColour.webp';
import axios from 'axios';

function SignInForm2() {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [errorMessage, setErrorMessage] = useState('');

    const handleLogin = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault(); // Prevent the form from submitting the default way
        try {
            // Create the body for the login request
            const loginBody = {
                Username: username,
                Password: password
            };

            // Send a POST request to your backend login endpoint
            const response = await axios.post("http://localhost:5097/api/v1/Login/", loginBody, {
                headers: {
                    'Content-Type': 'application/json',
                },
            });


            // If login is successful, handle response (you can save session data here)
            alert(response.data);
            // You can also redirect the user or show a success message

        } catch (error: unknown) {
            alert(error)
        }
    };

    return (
        <>
            <img
                className="sign-in-image" src={CalendarBackground} alt="Calendar Background"
            />
            <div className="sign-in-container">
                <h1 className="sign-in-title">Sign In</h1>
                <form className="sign-in-form" onSubmit={handleLogin}>
                    <div className="form-group">
                        <label htmlFor="email" className="form-label">Email</label>
                        <input
                            type="email"
                            id="email"
                            className="form-input"
                            placeholder="Enter your email"
                            value={username}
                            onChange={(e) => setUsername(e.target.value)}
                        />
                    </div>
                    <div className="form-group">
                        <label htmlFor="password" className="form-label">Password</label>
                        <input
                            type="password"
                            id="password"
                            className="form-input"
                            placeholder="Enter your password"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                        />
                    </div>
                    {errorMessage && <p className="error-message">{errorMessage}</p>}
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

export default SignInForm2;
