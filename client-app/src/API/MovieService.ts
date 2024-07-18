// src/API/MovieService.ts
import { MovieModel } from '../Models/MovieModel';
import apiClient from './AxiosConfig';

export const getMovies = async () => {
    try {
        const response = await apiClient.get('/Movie/movie');
        return response.data;
    } catch (error) {
        console.error('Error fetching movies:', error);
        throw error;
    }
};


export const getMovieById = async (id: number): Promise<MovieModel> => {
    const response = await apiClient.get<MovieModel>(`/Movie/${id}`);
    return response.data;
};

export const createMovie = async (movie: MovieModel) => {
    try {
        const response = await apiClient.post('/movie', movie);
        return response.data;
    } catch (error) {
        console.error('Error creating movie:', error);
        throw error;
    }
};

export const updateMovie = async (id: number, movie: MovieModel) => {
    try {
        await apiClient.put(`/movie/${id}`, movie);
    } catch (error) {
        console.error('Error updating movie:', error);
        throw error;
    }
};

export const deleteMovie = async (id: number) => {
    try {
        await apiClient.delete(`/movie/${id}`);
    } catch (error) {
        console.error('Error deleting movie:', error);
        throw error;
    }
};

export const getTopRatedMovies = async (count: number) => {
    try {
        const response = await apiClient.get(`/movie/top/${count}`);
        return response.data;
    } catch (error) {
        console.error('Error fetching top rated movies:', error);
        throw error;
    }
};

export const searchMovies = async (keyword: string) => {
    try {
        const response = await apiClient.get(`/movie/search`, { params: { keyword } });
        return response.data;
    } catch (error) {
        console.error('Error searching movies:', error);
        throw error;
    }
};

export const filterMovies = async (genre: string, minRating: number) => {
    try {
        const response = await apiClient.get(`/movie/filter`, { params: { genre, minRating } });
        return response.data;
    } catch (error) {
        console.error('Error filtering movies:', error);
        throw error;
    }
};

export const getMoviesByGenre = async (genre: string) => {
    try {
        const response = await apiClient.get(`/movie/genre/${genre}`);
        return response.data;
    } catch (error) {
        console.error('Error fetching movies by genre:', error);
        throw error;
    }
};

export const addUserRating = async (movieTitle: string, userId: number, rating: number) => {
    try {
        await apiClient.post(`/movie/${movieTitle}/rate`, { userId, rating });
    } catch (error) {
        console.error('Error adding user rating:', error);
        throw error;
    }
};


export const uploadMovie = async (file: File) => {
    const formData = new FormData();
    formData.append('file', file);

    try {
        const response = await apiClient.post('/movie/upload', formData, {
            headers: {
                'Content-Type': 'multipart/form-data'
            }
        });
        return response.data;
    } catch (error) {
        console.error('Error uploading movie:', error);
        throw error;
    }
};

export const getSecureStreamUrl = async (id: number): Promise<{ url: string }> => {
    const response = await apiClient.get<{ url: string }>(`/movies/${id}/stream`);
    return response.data;
};

export const toggleFavoriteMovie = async (movieId: number, userId: number) => {
    await apiClient.post(`/User/${userId}/favorite/${movieId}`);
};

export const checkIfFavorite = async (movieId: number, userId: number): Promise<boolean> => {
    const response = await apiClient.get(`/User/${userId}/favorite/${movieId}`);
    return response.data.isFavorite;
};