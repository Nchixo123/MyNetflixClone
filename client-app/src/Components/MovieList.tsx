// src/Components/MovieList.tsx
import React from 'react';
import { MovieModel } from '../Models/MovieModel';

interface MovieListProps {
    movies: MovieModel[];
    onMovieClick: (index: number) => void;
}

const MovieList: React.FC<MovieListProps> = ({ movies, onMovieClick }) => {
    return (
        <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6">
            {movies.map((movie, index) => (
                <div
                    key={movie.id}
                    className="bg-white shadow-lg rounded-lg p-4 cursor-pointer"
                    onClick={() => onMovieClick(index)}
                >
                    <img src={movie.imageUrl} alt={movie.title} className="w-full h-40 object-cover rounded-lg mb-4" />
                    <h2 className="text-xl font-semibold text-[#002379]">{movie.title}</h2>
                    <p className="text-gray-700">{movie.description}</p>
                </div>
            ))}
        </div>
    );
};

export default MovieList;
