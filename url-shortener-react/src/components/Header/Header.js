import React from 'react';
import { Link } from 'react-router-dom';
import useAuthStore from '../../stores/authStore';
import './Header.css';

const Header = () => {
    const isAuthenticated = useAuthStore((state) => state.isAuthenticated);
    const logout = useAuthStore((state) => state.logout);

    return (
        <header>
            <nav>
                <Link to="/">ShortUrls</Link>
                {isAuthenticated ? (
                    <>
                        <button onClick={logout}>Logout</button>
                    </>
                ) : (
                    <>
                        <Link to="/login">Login</Link>
                    </>
                )}
            </nav>
        </header>
    );
};

export default Header;