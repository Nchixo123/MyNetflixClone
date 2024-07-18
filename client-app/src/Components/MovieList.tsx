// src/Components/MovieList.tsx
import React from 'react';
import { Link } from 'react-router-dom';
import { MovieModel } from '../Models/MovieModel';

interface MovieListProps {
    movies: MovieModel[];
    onMovieClick: (index: number) => void;
}

const MovieList: React.FC<MovieListProps> = ({ movies, onMovieClick }) => {
    return (
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
            {movies.map((movie, index) => (
                <Link to={`/movie/${movie.id}`} key={movie.id} onClick={() => onMovieClick(index)}>
                    <div className="border rounded-lg p-4 shadow hover:shadow-lg transition-shadow">
                        <img src={movie.imageUrl} alt={movie.title} className="w-full h-40 object-cover mb-4" />
                        <h2 className="text-xl font-bold">{movie.title}</h2>
                        <p className="text-gray-700">{movie.description}</p>
                    </div>
                </Link>
            ))}
        </div>
    );
};

export default MovieList;
