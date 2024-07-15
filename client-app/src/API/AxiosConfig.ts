// src/api/AxiosConfig.ts
import axios from 'axios';

const apiClient = axios.create({
    baseURL: 'https://localhost:5000/api',
    withCredentials: true,
    headers: {
        'Content-Type': 'application/json',
    },
});

apiClient.interceptors.request.use((config) => {
    const token = localStorage.getItem('token');
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
});

export default apiClient;
