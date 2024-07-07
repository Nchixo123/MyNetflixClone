//import React, { useState } from 'react';
//import { useNavigate } from 'react-router-dom';
//import { login } from '../API/authService';
//import { useAuth } from '../Components/AuthContext';

//const LoginPage: React.FC = () => {
//    const [email, setEmail] = useState('');
//    const [password, setPassword] = useState('');
//    const [error, setError] = useState('');
//    const { login: loginUser } = useAuth();
//    const navigate = useNavigate();

//    const handleSubmit = async (e: React.FormEvent) => {
//        e.preventDefault();
//        try {
//            const { token } = await login(email, password);
//            loginUser(token);
//            navigate('/profile');
//        } catch (err) {
//            setError('Invalid email or password');
//        }
//    };

//    return (
//        <div className="min-h-screen flex flex-col items-center justify-center bg-light-orange">
//            <h1 className="text-4xl font-bold mb-8">Login</h1>
//            {error && <p className="text-red-500">{error}</p>}
//            <form onSubmit={handleSubmit} className="w-full max-w-sm">
//                <input
//                    type="email"
//                    placeholder="Email"
//                    value={email}
//                    onChange={(e) => setEmail(e.target.value)}
//                    className="w-full p-2 mb-4 border border-gray-300"
//                />
//                <input
//                    type="password"
//                    placeholder="Password"
//                    value={password}
//                    onChange={(e) => setPassword(e.target.value)}
//                    className="w-full p-2 mb-4 border border-gray-300"
//                />
//                <button
//                    type="submit"
//                    className="w-full bg-navy text-beige p-2 font-bold"
//                >
//                    Login
//                </button>
//            </form>
//        </div>
//    );
//};

//export default LoginPage;
