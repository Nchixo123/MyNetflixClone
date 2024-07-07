// src/api/AxiosConfig.ts
import axios from 'axios';

const apiClient = axios.create({
    baseURL: 'http://localhost:5192/api', // Update with your backend URL
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
