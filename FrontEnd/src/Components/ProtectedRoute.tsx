import React from 'react';
import { Navigate, Route, RouteProps } from 'react-router-dom';

interface PrivateRouteProps {
    element: JSX.Element;
    isAuthenticated: boolean;
}

const PrivateRoute: React.FC<PrivateRouteProps> = ({ element, isAuthenticated, }) => {
    if (!isAuthenticated) {
        // Redirect to the login page if not authenticated
        return <Navigate to="/login" />;
    }

    // If authenticated, render the element
    return element;
};

export default PrivateRoute;
