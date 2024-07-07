import React from 'react';
import Navbar from '../Components/Navbar';

const UserProfilePage: React.FC = () => {
    return (
        <div>
            <Navbar />
            <div className="container mx-auto mt-8">
                <h1 className="text-4xl font-bold text-center">User Profile</h1>
            </div>
        </div>
    );
};

export default UserProfilePage;