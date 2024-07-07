import React from 'react';
import { Route, Navigate } from 'react-router-dom';
import { useAuth } from '../Components/AuthContext';

interface ProtectedRouteProps {
    component: React.ElementType;
    path: string;
}

const ProtectedRoute: React.FC<ProtectedRouteProps> = ({ component: Component, ...rest }) => {
    const { token } = useAuth();

    return (
        <Route
            {...rest}
            element={token ? <Component /> : <Navigate to="/login" />}
        />
    );
};

export default ProtectedRoute;
