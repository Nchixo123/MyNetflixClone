import React, { useState, useEffect } from 'react';
import Navbar from '../Components/Navbar';
import { getMovies, createMovie, updateMovie, deleteMovie } from '../API/MovieService';
import { MovieModel } from '../Models/MovieModel';

const AdminPage: React.FC = () => {
    const [movies, setMovies] = useState<MovieModel[]>([]);
    const [selectedMovie, setSelectedMovie] = useState<MovieModel | null>(null);
    const [title, setTitle] = useState('');
    const [description, setDescription] = useState('');
    const [director, setDirector] = useState('');
    const [genre, setGenre] = useState('');
    const [imageUrl, setImageUrl] = useState('');
    const [videoUrl, setVideoUrl] = useState('');

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

    const handleCreateMovie = async () => {
        try {
            const newMovie: MovieModel = {
                id: 0, // The backend should generate the ID
                title,
                description,
                director,
                genre,
                imageUrl,
                videoUrl,
                averageRating: 0,
            };
            await createMovie(newMovie);
            alert('Movie created successfully!');
            setTitle('');
            setDescription('');
            setDirector('');
            setGenre('');
            setImageUrl('');
            setVideoUrl('');
        } catch (error) {
            console.error('Error creating movie:', error);
            alert('Failed to create movie');
        }
    };

    const handleUpdateMovie = async () => {
        if (selectedMovie) {
            try {
                const updatedMovie: MovieModel = {
                    ...selectedMovie,
                    title,
                    description,
                    director,
                    genre,
                    imageUrl,
                    videoUrl
                };
                await updateMovie(selectedMovie.id, updatedMovie);
                alert('Movie updated successfully!');
                setSelectedMovie(null);
                setTitle('');
                setDescription('');
                setDirector('');
                setGenre('');
                setImageUrl('');
                setVideoUrl('');
            } catch (error) {
                console.error('Error updating movie:', error);
                alert('Failed to update movie');
            }
        }
    };

    const handleDeleteMovie = async (id: number) => {
        try {
            await deleteMovie(id);
            alert('Movie deleted successfully!');
            setMovies(movies.filter((movie) => movie.id !== id));
        } catch (error) {
            console.error('Error deleting movie:', error);
            alert('Failed to delete movie');
        }
    };

    const handleSelectMovie = (movie: MovieModel) => {
        setSelectedMovie(movie);
        setTitle(movie.title);
        setDescription(movie.description);
        setDirector(movie.director);
        setGenre(movie.genre);
        setImageUrl(movie.imageUrl);
        setVideoUrl(movie.videoUrl);
    };

    return (
        <div className="min-h-screen bg-[#FFFAE6] flex flex-col">
            <Navbar />
            <div className="container mx-auto mt-8">
                <h1 className="text-4xl font-bold text-center mb-8">Admin Page</h1>
                <div className="flex justify-center mb-8">
                    <div className="w-full max-w-md">
                        <div className="mb-4">
                            <label htmlFor="title" className="block text-gray-700">Title</label>
                            <input
                                type="text"
                                id="title"
                                value={title}
                                onChange={(e) => setTitle(e.target.value)}
                                className="w-full px-3 py-2 border rounded"
                            />
                        </div>
                        <div className="mb-4">
                            <label htmlFor="description" className="block text-gray-700">Description</label>
                            <textarea
                                id="description"
                                value={description}
                                onChange={(e) => setDescription(e.target.value)}
                                className="w-full px-3 py-2 border rounded"
                            />
                        </div>
                        <div className="mb-4">
                            <label htmlFor="director" className="block text-gray-700">Director</label>
                            <input
                                type="text"
                                id="director"
                                value={director}
                                onChange={(e) => setDirector(e.target.value)}
                                className="w-full px-3 py-2 border rounded"
                            />
                        </div>
                        <div className="mb-4">
                            <label htmlFor="genre" className="block text-gray-700">Genre</label>
                            <input
                                type="text"
                                id="genre"
                                value={genre}
                                onChange={(e) => setGenre(e.target.value)}
                                className="w-full px-3 py-2 border rounded"
                            />
                        </div>
                        <div className="mb-4">
                            <label htmlFor="imageUrl" className="block text-gray-700">Image URL</label>
                            <input
                                type="text"
                                id="imageUrl"
                                value={imageUrl}
                                onChange={(e) => setImageUrl(e.target.value)}
                                className="w-full px-3 py-2 border rounded"
                            />
                        </div>
                        <div className="mb-4">
                            <label htmlFor="videoUrl" className="block text-gray-700">Video URL</label>
                            <input
                                type="text"
                                id="videoUrl"
                                value={videoUrl}
                                onChange={(e) => setVideoUrl(e.target.value)}
                                className="w-full px-3 py-2 border rounded"
                            />
                        </div>
                        <div className="flex justify-between">
                            {selectedMovie ? (
                                <button
                                    onClick={handleUpdateMovie}
                                    className="bg-[#002379] text-white px-4 py-2 rounded"
                                >
                                    Update Movie
                                </button>
                            ) : (
                                <button
                                    onClick={handleCreateMovie}
                                    className="bg-[#002379] text-white px-4 py-2 rounded"
                                >
                                    Create Movie
                                </button>
                            )}
                            <button
                                onClick={() => setSelectedMovie(null)}
                                className="bg-gray-500 text-white px-4 py-2 rounded"
                            >
                                Clear
                            </button>
                        </div>
                    </div>
                </div>
                <div className="flex justify-center">
                    <div className="w-full max-w-2xl">
                        <h2 className="text-2xl font-bold text-center mb-4">Movies</h2>
                        <ul>
                            {movies.map((movie) => (
                                <li key={movie.id} className="border-b py-2 flex justify-between items-center">
                                    <span>{movie.title}</span>
                                    <div>
                                        <button
                                            onClick={() => handleSelectMovie(movie)}
                                            className="bg-blue-500 text-white px-2 py-1 rounded mr-2"
                                        >
                                            Edit
                                        </button>
                                        <button
                                            onClick={() => handleDeleteMovie(movie.id)}
                                            className="bg-red-500 text-white px-2 py-1 rounded"
                                        >
                                            Delete
                                        </button>
                                    </div>
                                </li>
                            ))}
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default AdminPage;
