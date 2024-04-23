import axios from 'axios';
import Cookies from 'js-cookie';

export const API_URL = 'https://localhost:7218/api';

export const axiosInstance = axios.create({
    baseURL: API_URL,
    headers: {
        'Content-Type': 'application/json'
    }
});

axiosInstance.interceptors.request.use((config) => {
    const user = Cookies.get('user');
    if (user) {
        const { accessToken } = JSON.parse(user);
        config.headers['Authorization'] = `Bearer ${accessToken}`;
    }
    return config;
});