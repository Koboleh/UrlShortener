import create from 'zustand';
import { authService } from '../services/authService';

const useAuthStore = create((set) => ({
    user: authService.getUser(),
    isAuthenticated: !!authService.getUser(),
    login: async (username, password) => {
        const userData = await authService.login(username, password);
        set({ user: userData, isAuthenticated: true });
    },

    logout: () => {
        authService.logout();
        set({ user: null, isAuthenticated: false });
    },

    checkAuth: () => {
        const user = authService.getUser();
        if (user && new Date().getTime() < new Date(user.expiresAt).getTime()) {
            set({ user, isAuthenticated: true });
        } else {
            authService.logout();
            set({ user: null, isAuthenticated: false });
        }
    },
}));

export default useAuthStore;