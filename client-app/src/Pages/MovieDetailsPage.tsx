// src/Pages/MovieDetailsPage.tsx
import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import Navbar from '../Components/Navbar';
import { getMovieById, addUserRating, toggleFavoriteMovie, checkIfFavorite } from '../API/MovieService';
import { MovieModel } from '../Models/MovieModel';
import ReactStars from 'react-stars';
import ReactPlayer from 'react-player';
import { useAuth } from '../Components/AuthContext';
import './MovieDetailsPage.css';

const MovieDetailsPage: React.FC = () => {
    const { id } = useParams<{ id: string }>();
    const [movie, setMovie] = useState<MovieModel | null>(null);
    const [rating, setRating] = useState<number>(0);
    const [isFavorite, setIsFavorite] = useState<boolean>(false);
    const { token } = useAuth();

    useEffect(() => {
        const fetchMovie = async () => {
            if (id) {
                try {
                    const movieData = await getMovieById(parseInt(id));
                    setMovie(movieData);

                    // Check if the movie is a favorite
                    if (token) {
                        const userId = parseTokenForUserId(token);
                        const isFav = await checkIfFavorite(movieData.id, userId);
                        setIsFavorite(isFav);
                    }
                } catch (error) {
                    console.error('Error fetching movie:', error);
                }
            }
        };

        fetchMovie();
    }, [id, token]);

    const handleRating = async (newRating: number) => {
        setRating(newRating);
        if (movie && token) {
            const userId = parseTokenForUserId(token);
            try {
                await addUserRating(movie.title, userId, newRating);
                alert('Rating submitted successfully!');
            } catch (error) {
                console.error('Error adding rating:', error);
            }
        }
    };

    const handleToggleFavorite = async () => {
        if (movie && token) {
            const userId = parseTokenForUserId(token);
            try {
                await toggleFavoriteMovie(movie.id, userId);
                setIsFavorite(!isFavorite);
            } catch (error) {
                console.error('Error toggling favorite:', error);
            }
        }
    };

    const parseTokenForUserId = (token: string): number => {
        const decodedToken = JSON.parse(atob(token.split('.')[1]));
        return decodedToken.userId;
    };

    if (!movie) {
        return <div>Loading...</div>;
    }

    return (
        <div className="min-h-screen bg-[#FFFAE6] flex flex-col">
            <Navbar />
            <div className="flex-grow flex mt-10 ml-10">
                <div className="w-full max-w-sm flex flex-col items-start">
                    <img src={movie.imageUrl} alt={movie.title} className="w-64 h-auto object-cover rounded-lg mb-4" />
                    <h1 className="text-4xl font-bold text-[#002379] mb-4">{movie.title}</h1>
                    <p className="text-lg text-gray-700 mb-4">{movie.description}</p>
                    <p className="text-lg text-gray-700 mb-2">Directed by: {movie.director}</p>
                    <p className="text-lg text-gray-700 mb-2">Genre: {movie.genre}</p>
                    <div className="mt-4">
                        <label htmlFor="rating" className="block text-gray-700 mb-2">Rate this movie:</label>
                        <ReactStars
                            count={5}
                            onChange={handleRating}
                            size={24}
                            color2={'#ffd700'}
                            value={rating}
                        />
                        <button
                            onClick={() => handleRating(rating)}
                            className="bg-[#002379] text-white px-4 py-2 rounded mt-2 click-animation"
                        >
                            Submit Rating
                        </button>
                        <button
                            onClick={handleToggleFavorite}
                            className={`mt-2 px-4 py-2 rounded ${isFavorite ? 'bg-red-500' : 'bg-green-500'} text-white click-animation`}
                        >
                            {isFavorite ? 'Remove from Favorites' : 'Add to Favorites'}
                        </button>
                    </div>
                </div>
                <div className="flex-grow flex justify-center items-start">
                    <ReactPlayer url={movie.videoUrl} controls width="50%" height="400px" className="mb-8 rounded-lg" />
                </div>
            </div>
        </div>
    );
};

export default MovieDetailsPage;
