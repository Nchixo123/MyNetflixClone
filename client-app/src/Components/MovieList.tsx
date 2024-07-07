import React from 'react';

interface Movie {
    title: string;
    imageUrl: string;
    description: string;
}

interface MovieListProps {
    movies: Movie[];
    onMovieClick: (index: number) => void;
}

const MovieList: React.FC<MovieListProps> = ({ movies, onMovieClick }) => {
    return (
        <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-4">
            {movies.map((movie, index) => (
                <div key={index} className="p-4 bg-white shadow rounded" onClick={() => onMovieClick(index)}>
                    <img src={movie.imageUrl} alt={movie.title} className="w-full h-32 object-cover mb-2" />
                    <h2 className="text-xl font-bold">{movie.title}</h2>
                    <p>{movie.description}</p>
                </div>
            ))}
        </div>
    );
};

export default MovieList;
