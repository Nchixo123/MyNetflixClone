// src/App.tsx
import React from 'react';
import { Navigate, Route, Routes } from 'react-router-dom';
import HomePage from './Pages/HomePage';
import UserProfilePage from './Pages/UserProfilePage';
import LoginPage from './Pages/LoginPage';
import SignUpPage from './Pages/SignUpPage';
import { useAuth } from './Components/AuthContext';
import MovieDetailsPage from './Pages/MovieDetailsPage';

const App: React.FC = () => {
    const { isAuthenticated } = useAuth();

    return (
            <Routes>
                <Route path="/" element={<HomePage />} />
                <Route path="/profile" element={isAuthenticated ? <UserProfilePage /> : <Navigate to="/login" />} />
                <Route path="/login" element={<LoginPage />} />
                <Route path="/signup" element={<SignUpPage />} />
                <Route path="/movies/:id" element={<MovieDetailsPage />} />
            </Routes>
    );
};

export default App;

