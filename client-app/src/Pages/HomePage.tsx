import React, { useState, useEffect } from 'react';
import Navbar from '../Components/Navbar';
import MovieList from '../Components/MovieList';
import SearchBar from '../Components/SearchBar';
import FilterOptions from '../Components/FilterOptions';
import { getMovies, searchMovies, filterMovies } from '../API/MovieService';
import { MovieModel } from '../Models/MovieModel';

const HomePage: React.FC = () => {
    const [movies, setMovies] = useState<MovieModel[]>([]);

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

    const handleSearch = async (keyword: string) => {
        try {
            const moviesData = await searchMovies(keyword);
            setMovies(moviesData);
        } catch (error) {
            console.error('Error searching movies:', error);
        }
    };

    const handleFilter = async (genre: string, minRating: number) => {
        try {
            const moviesData = await filterMovies(genre, minRating);
            setMovies(moviesData);
        } catch (error) {
            console.error('Error filtering movies:', error);
        }
    };

    const handleMovieClick = (index: number) => {
        console.log(`Movie ${index + 1} clicked`);
    };

    return (
        <div className="min-h-screen bg-[#FFFAE6] flex flex-col">
            <Navbar />
            <div className="flex-grow flex flex-col items-center mt-4">
                <h1 className="text-4xl font-bold text-[#002379] text-center mb-8">Welcome to Netflix Clone</h1>
                <div className="w-full max-w-4xl flex justify-center">
                    <div className="w-full max-w-xs mr-8">
                        <FilterOptions onFilter={handleFilter} />
                    </div>
                    <div className="flex-grow">
                        <SearchBar onSearch={handleSearch} />
                        <MovieList movies={movies} onMovieClick={handleMovieClick} />
                    </div>
                </div>
            </div>
        </div>
    );
};

export default HomePage;
