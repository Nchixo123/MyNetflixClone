// src/Pages/MovieDetailsPage.tsx
import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import Navbar from '../Components/Navbar';
import { getMovieById, getSecureStreamUrl, addUserRating } from '../API/MovieService';
import { MovieModel } from '../Models/MovieModel';
import ReactPlayer from 'react-player';
import { useAuth } from '../Components/AuthContext';

const MovieDetailsPage: React.FC = () => {
    const { id } = useParams<{ id: string }>();
    const [movie, setMovie] = useState<MovieModel | null>(null);
    const [secureUrl, setSecureUrl] = useState<string | null>(null);
    const [rating, setRating] = useState<number>(0);
    const { token } = useAuth();

    useEffect(() => {
        const fetchMovie = async () => {
            if (id) {
                try {
                    const movieData = await getMovieById(parseInt(id));
                    setMovie(movieData);

                    const secureUrlData = await getSecureStreamUrl(parseInt(id));
                    setSecureUrl(secureUrlData.url);
                } catch (error) {
                    console.error('Error fetching movie:', error);
                }
            }
        };

        fetchMovie();
    }, [id]);

    const handleRating = async (rating: number) => {
        if (movie && token) {
            const userId = parseTokenForUserId(token);
            try {
                await addUserRating(movie.title, userId, rating);
                alert('Rating submitted successfully!');
            } catch (error) {
                console.error('Error adding rating:', error);
            }
        }
    };

    const parseTokenForUserId = (token: string): number => {
        const decodedToken = JSON.parse(atob(token.split('.')[1]));
        return decodedToken.userId;
    };

    if (!movie || !secureUrl) {
        return <div>Loading...</div>;
    }

    return (
        <div className="min-h-screen bg-[#FFFAE6] flex flex-col">
            <Navbar />
            <div className="flex-grow flex items-center justify-center">
                <div className="w-full max-w-4xl mt-20 flex flex-col items-center">
                    <h1 className="text-4xl font-bold text-[#002379] text-center mb-8">{movie.title}</h1>
                    <img src={movie.imageUrl} alt={movie.title} className="w-full h-80 object-cover rounded-lg mb-4" />
                    <p className="text-lg text-gray-700 mb-4">{movie.description}</p>
                    <p className="text-lg text-gray-700 mb-4">Directed by: {movie.director}</p>
                    <p className="text-lg text-gray-700 mb-4">Genre: {movie.genre}</p>
                    <div className="w-full h-80 rounded-lg mb-4">
                        <ReactPlayer url={secureUrl} controls width="100%" height="100%" />
                    </div>
                    <div className="mt-4">
                        <label htmlFor="rating" className="block text-gray-700">Rate this movie:</label>
                        <select
                            id="rating"
                            value={rating}
                            onChange={(e) => setRating(parseInt(e.target.value))}
                            className="w-full px-3 py-2 border rounded"
                        >
                            <option value="0">Select Rating</option>
                            <option value="1">1</option>
                            <option value="2">2</option>
                            <option value="3">3</option>
                            <option value="4">4</option>
                            <option value="5">5</option>
                        </select>
                        <button
                            onClick={() => handleRating(rating)}
                            className="bg-[#002379] text-white px-4 py-2 rounded mt-2"
                        >
                            Submit Rating
                        </button>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default MovieDetailsPage;
