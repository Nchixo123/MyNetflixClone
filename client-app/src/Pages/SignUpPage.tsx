import React, { useState } from 'react';
import Navbar from '../Components/Navbar';
import { createUser } from '../API/UserService';
import { Link, useNavigate } from 'react-router-dom';

const SignUpPage: React.FC = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [email, setEmail] = useState('');
    const [profilePictureUrl, setProfilePictureUrl] = useState('');
    const navigate = useNavigate();

    const generateRandomId = () => {
        return Math.floor(Math.random() * 1000000); // Generate a random number between 0 and 999999
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        try {
            const userData = {
                id: generateRandomId(), // Generate a random ID
                username,
                password,
                email,
                profilePictureUrl,
                gender: 0, // Assuming default gender value
                isDelete: false,
                favoriteMovies: []
            };
            await createUser(userData);
            navigate('/login');
        } catch (error) {
            console.error('Error registering user:', error);
        }
    };

    return (
        <div className="min-h-screen bg-[#FFFAE6] flex flex-col">
            <Navbar />
            <div className="container mx-auto mt-8">
                <h1 className="text-4xl font-bold text-center mb-8">Sign Up</h1>
                <form onSubmit={handleSubmit} className="max-w-md mx-auto">
                    <div className="mb-4">
                        <label htmlFor="username" className="block text-gray-700">Username</label>
                        <input
                            type="text"
                            id="username"
                            value={username}
                            onChange={(e) => setUsername(e.target.value)}
                            className="w-full px-3 py-2 border rounded"
                        />
                    </div>
                    <div className="mb-4">
                        <label htmlFor="email" className="block text-gray-700">Email</label>
                        <input
                            type="email"
                            id="email"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            className="w-full px-3 py-2 border rounded"
                        />
                    </div>
                    <div className="mb-4">
                        <label htmlFor="password" className="block text-gray-700">Password</label>
                        <input
                            type="password"
                            id="password"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            className="w-full px-3 py-2 border rounded"
                        />
                    </div>
                    <div className="mb-4">
                        <label htmlFor="profilePictureUrl" className="block text-gray-700">Profile Picture URL</label>
                        <input
                            type="text"
                            id="profilePictureUrl"
                            value={profilePictureUrl}
                            onChange={(e) => setProfilePictureUrl(e.target.value)}
                            className="w-full px-3 py-2 border rounded"
                        />
                    </div>
                    <div className="flex justify-between">
                        <button type="submit" className="bg-[#002379] text-white px-4 py-2 rounded">
                            Sign Up
                        </button>
                        <Link to="/" className="bg-gray-500 text-white px-4 py-2 rounded">
                            Return Home
                        </Link>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default SignUpPage;
