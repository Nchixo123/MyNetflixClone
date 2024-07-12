import React from 'react';
import { Link } from 'react-router-dom';

const Navbar: React.FC = () => {
    return (
        <nav className="bg-navy text-beige p-4">
            <div className="container mx-auto flex justify-between items-center">
                <h1 className="text-2xl font-bold">Netflix Clone</h1>
                <div>
                    <Link to="/" className="mr-4 hover:text-orange">Home</Link>
                    <Link to="/profile" className="mr-4 hover:text-orange">Profile</Link>
                    <Link to="/login" className="mr-4 hover:text-orange">Login</Link>
                    <Link to="/signup" className="hover:text-orange">Sign Up</Link>
                </div>
            </div>
        </nav>
    );
};

export default Navbar;
