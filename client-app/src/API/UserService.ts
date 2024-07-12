import apiClient from './AxiosConfig';
import { UserModel } from '../Models/UserModel';
import { MovieModel } from '../Models/MovieModel';

export const getUserById = async (id: number): Promise<UserModel> => {
    const response = await apiClient.get<UserModel>(`/api/user/${id}`);
    return response.data;
};

export const updateUser = async (id: number, userData: UserModel): Promise<void> => {
    await apiClient.put(`/api/user/${id}`, userData);
};

export const createUser = async (userData: UserModel): Promise<UserModel> => {
    const response = await apiClient.post<UserModel>('/api/user', userData);
    return response.data;
};

export const getAllUsers = async (): Promise<UserModel[]> => {
    const response = await apiClient.get<UserModel[]>('/api/user');
    return response.data;
};

export const deleteUser = async (id: number): Promise<void> => {
    await apiClient.delete(`/api/user/${id}`);
};

export const getFavoriteMovies = async (id: number): Promise<MovieModel[]> => {
    const response = await apiClient.get<MovieModel[]>(`/api/user/${id}/favorites`);
    return response.data;
};

export const addFavoriteMovie = async (id: number, movieId: number): Promise<void> => {
    await apiClient.post(`/api/user/${id}/favorites/${movieId}`);
};

export const getUserByEmail = async (email: string): Promise<UserModel> => {
    const response = await apiClient.get<UserModel>(`/api/user/email/${email}`);
    return response.data;
};

export const changePassword = async (id: number, currentPassword: string, newPassword: string): Promise<void> => {
    await apiClient.post(`/api/user/${id}/change-password`, { currentPassword, newPassword });
};
