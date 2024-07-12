import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { login } from '../API/authService';
import { useAuth } from '../Components/AuthContext';

const LoginPage: React.FC = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const navigate = useNavigate();
    const { login: authLogin } = useAuth();

    const handleLogin = async (e: React.FormEvent) => {
        e.preventDefault();
        try {
            const token = await login(email, password);
            authLogin(token);
            navigate('/');
        } catch (error) {
            setError('Failed to login');
        }
    };

    const handleGoHome = () => {
        navigate('/');
    };

    return (
        <div className="min-h-screen bg-[#FFFAE6] flex items-center justify-center">
            <form onSubmit={handleLogin} className="bg-white p-6 rounded shadow-md w-full max-w-sm">
                <h2 className="text-2xl font-bold text-[#002379] mb-4">Login</h2>
                {error && <p className="text-red-500 mb-4">{error}</p>}
                <div className="mb-4">
                    <label className="block mb-2">Email</label>
                    <input
                        type="email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        className="w-full p-2 border border-gray-300 rounded"
                    />
                </div>
                <div className="mb-4">
                    <label className="block mb-2">Password</label>
                    <input
                        type="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        className="w-full p-2 border border-gray-300 rounded"
                    />
                </div>
                <button type="submit" className="w-full p-2 bg-blue-500 text-white rounded mb-4">Login</button>
                <button
                    onClick={handleGoHome}
                    className="w-full p-2 bg-gray-500 text-white rounded"
                >
                    Return
                </button>
            </form>
        </div>
    );
};

export default LoginPage;
