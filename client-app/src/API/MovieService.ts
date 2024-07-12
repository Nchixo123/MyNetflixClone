// src/API/MovieService.ts
import { MovieModel } from '../Models/MovieModel';
import apiClient from './AxiosConfig';

export const getMovies = async () => {
    try {
        const response = await apiClient.get('/api/movie');
        return response.data;
    } catch (error) {
        console.error('Error fetching movies:', error);
        throw error;
    }
};

export const getMovieById = async (id: number): Promise<MovieModel> => {
    try {
        const response = await apiClient.get<MovieModel>(`/movies/${id}`);
        return response.data;
    } catch (error) {
        console.error('Error fetching movie by id:', error);
        throw error;
    }
};

export const createMovie = async (movie: MovieModel) => {
    try {
        const response = await apiClient.post('/api/movie', movie);
        return response.data;
    } catch (error) {
        console.error('Error creating movie:', error);
        throw error;
    }
};

export const updateMovie = async (id: number, movie: MovieModel) => {
    try {
        await apiClient.put(`/api/movie/${id}`, movie);
    } catch (error) {
        console.error('Error updating movie:', error);
        throw error;
    }
};

export const deleteMovie = async (id: number) => {
    try {
        await apiClient.delete(`/api/movie/${id}`);
    } catch (error) {
        console.error('Error deleting movie:', error);
        throw error;
    }
};

export const getTopRatedMovies = async (count: number) => {
    try {
        const response = await apiClient.get(`/api/movie/top/${count}`);
        return response.data;
    } catch (error) {
        console.error('Error fetching top rated movies:', error);
        throw error;
    }
};

export const searchMovies = async (keyword: string) => {
    try {
        const response = await apiClient.get(`/api/movie/search`, { params: { keyword } });
        return response.data;
    } catch (error) {
        console.error('Error searching movies:', error);
        throw error;
    }
};

export const filterMovies = async (genre: string, minRating: number) => {
    try {
        const response = await apiClient.get(`/api/movie/filter`, { params: { genre, minRating } });
        return response.data;
    } catch (error) {
        console.error('Error filtering movies:', error);
        throw error;
    }
};

export const getMoviesByGenre = async (genre: string) => {
    try {
        const response = await apiClient.get(`/api/movie/genre/${genre}`);
        return response.data;
    } catch (error) {
        console.error('Error fetching movies by genre:', error);
        throw error;
    }
};

export const addUserRating = async (movieTitle: string, userId: number, rating: number) => {
    try {
        await apiClient.post(`/api/movie/${movieTitle}/rate`, { userId, rating });
    } catch (error) {
        console.error('Error adding user rating:', error);
        throw error;
    }
};


export const uploadMovie = async (file: File) => {
    const formData = new FormData();
    formData.append('file', file);

    try {
        const response = await apiClient.post('/api/movie/upload', formData, {
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
