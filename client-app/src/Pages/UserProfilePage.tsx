import React, { useState, useEffect } from 'react';
import { useAuth } from '../Components/AuthContext';
import { getUserById, updateUser } from '../API/UserService';
import Navbar from '../Components/Navbar';
import { UserModel } from '../Models/UserModel';
import { Link } from 'react-router-dom';

const UserProfilePage: React.FC = () => {
    const { token } = useAuth();
    const [user, setUser] = useState<UserModel | null>(null);
    const [username, setUsername] = useState('');
    const [profilePictureUrl, setProfilePictureUrl] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    useEffect(() => {
        const fetchUser = async () => {
            if (token) {
                try {
                    const userId = parseTokenForUserId(token);
                    if (userId !== null) {
                        const userData = await getUserById(userId);
                        setUser(userData);
                        setUsername(userData.username || '');
                        setProfilePictureUrl(userData.profilePictureUrl || '');
                        setEmail(userData.email || '');
                    } else {
                        console.error('Invalid userId parsed from token');
                    }
                } catch (error) {
                    console.error('Error fetching user data:', error);
                }
            }
        };

        fetchUser();
    }, [token]);

    const handleUpdate = async () => {
        if (user) {
            const updatedUser: UserModel = {
                id: user.id,
                username,
                profilePictureUrl,
                email,
                password,
                gender: user.gender, // Assuming gender does not change
                isDelete: user.isDelete, // Assuming isDelete does not change
                favoriteMovies: user.favoriteMovies // Assuming favoriteMovies does not change
            };

            try {
                await updateUser(updatedUser.id, updatedUser);
                alert('Profile updated successfully');
            } catch (error) {
                console.error('Error updating user profile:', error);
                alert('Failed to update profile');
            }
        }
    };

    if (!user) {
        return <div>Loading...</div>;
    }

    return (
        <div className="min-h-screen bg-[#FFFAE6] flex flex-col">
            <Navbar />
            <div className="container mx-auto mt-8">
                <h1 className="text-4xl font-bold text-center mb-8">User Profile</h1>
                <div className="max-w-md mx-auto">
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
                        <button
                            onClick={handleUpdate}
                            className="bg-[#002379] text-white px-4 py-2 rounded"
                        >
                            Update Profile
                        </button>
                        <Link to="/" className="bg-gray-500 text-white px-4 py-2 rounded">
                            Return Home
                        </Link>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default UserProfilePage;

const parseTokenForUserId = (token: string): number | null => {
    try {
        const base64Url = token.split('.')[1];
        const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
        const jsonPayload = decodeURIComponent(atob(base64).split('').map(function (c) {
            return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
        }).join(''));

        const decodedToken = JSON.parse(jsonPayload);
        return decodedToken.userId || null; // Replace 'userId' with the actual key used in your token
    } catch (error) {
        console.error('Error parsing token:', error);
        return null; // or throw an error if the token is invalid
    }
};
