// src/api/authService.ts
import apiClient from './AxiosConfig';

export const login = async (email: string, password: string) => {
    try {
        const response = await apiClient.post('/auth/login', { email, password });
        return response.data;
    } catch (error) {
        console.error('Error logging in:', error);
        throw error;
    }
};

export const register = async (username: string, email: string, password: string) => {
    try {
        const response = await apiClient.post('/auth/register', { username, email, password });
        return response.data;
    } catch (error) {
        console.error('Error registering:', error);
        throw error;
    }
};

export const logout = () => {
    localStorage.removeItem('token');
};
