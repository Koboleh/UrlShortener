import { axiosInstance } from '../config/apiConfig';
import Cookies from 'js-cookie';
import { jwtDecode } from 'jwt-decode';

const login = async (username, password) => {
    const response = await axiosInstance.post('/login', { username, password });
    if (response.data.accessToken) {
        const decodedToken = jwtDecode(response.data.accessToken);
        const expiresAt = new Date(decodedToken.exp * 1000);
        Cookies.set('user', JSON.stringify(response.data), { expires: expiresAt });
    }
    return response.data;
};

const logout = () => {
    Cookies.remove('user');
};

const getUser = () => {
    const user = Cookies.get('user');
    return user ? JSON.parse(user) : null;
};

export const authService = {
    login,
    logout,
    getUser
};