import React from 'react';
import Api_url from "./Api_url"

export const Header: React.FC = () => {
    const handleLogout = async () => {
        try {
            // Call the backend logout endpoint
            const response = await fetch(`${Api_url}/api/v1/Logout`, {
                method: 'GET',
                credentials: 'include', // Include cookies for the session
            });

            if (response.ok) {
                // Handle success (for example, redirect to login page or clear session data)
                alert("Logged out")
                window.location.href = '/login'; // Redirect to login page (or home page, etc.)
            } else {
                alert("Logout failed:" + response.statusText);
            }
        } catch (error) {
            alert("Error during logout:" + error);
        }
    };

    return (
        <header className="navbar">
            <a href="#">Homepage</a>
            <a href="#">Events</a>
            <a href="#">Calendar</a>
            <a href="#" onClick={handleLogout}>Log out</a>
        </header>
    );
};
