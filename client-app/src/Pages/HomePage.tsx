import React, { useState, useEffect } from 'react';
import Navbar from '../Components/Navbar';
import MovieList from '../Components/MovieList';
import { getMovies } from '../API/MovieService';

const HomePage: React.FC = () => {
    const [movies, setMovies] = useState([]);

    useEffect(() => {
        const fetchMovies = async () => {
            try {
                const moviesData = await getMovies();
                setMovies(moviesData);
            } catch (error) {
                console.error('Error fetching movies:', error);
            }
        };

        fetchMovies();
    }, []);

    const handleMovieClick = (index: number) => {
        console.log(`Movie ${index + 1} clicked`);
    };

    return (
        <div className="min-h-screen bg-beige flex flex-col">
            <Navbar />
            <div className="flex-grow flex items-center justify-center">
                <div className="w-full max-w-4xl mt-20 flex flex-col items-center">
                    <h1 className="text-4xl font-bold text-center mb-8 text-navy">Welcome to Netflix Clone</h1>
                    <MovieList movies={movies} onMovieClick={handleMovieClick} />
                </div>
            </div>
        </div>
    );
};

export default HomePage;
