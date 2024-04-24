import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import Header from '../../components/Header/Header';
import PaginationCounter from '../../components/PaginationCounter/PaginationCounter';
import useAuthStore from '../../stores/authStore';
import { urlService } from '../../services/urlService';
import './UrlsListPage.css';

const UrlsListPage = () => {
    const [urls, setUrls] = useState([]);
    const [pagesCount, setPagesCount] = useState(0);
    const [currentPage, setCurrentPage] = useState(1);
    const [error, setError] = useState('');
    const [newUrlName, setNewUrlName] = useState('');
    const [newOriginalUrl, setNewOriginalUrl] = useState('');
    const userId = useAuthStore((state) => state.user?.id);
    const isAuthenticated = useAuthStore((state) => state.isAuthenticated);
    const navigate = useNavigate();

    const fetchUrls = async () => {
        try {
            const paginationParams = {
                pageNumber: currentPage,
                pageSize: 5
            };
            const data = await urlService.getUrls(paginationParams);
            setUrls(data.items);
            setPagesCount(data.pagesCount);
        } catch (err) {
            setError(err.message);
        }
    };

    useEffect(() => {
        fetchUrls();
    }, [currentPage, fetchUrls]);


    const handleRowClick = (urlId) => {
        isAuthenticated ? navigate(`/urls/${urlId}`) : navigate('/login');
    };

    const handleCreateUrl = async (e) => {
        e.preventDefault();
        try {
            const newUrl = {
                name: newUrlName,
                originalUrl: newOriginalUrl,
                userId: userId
            };
            await urlService.createUrl(newUrl);
            await fetchUrls();
            setNewUrlName('');
            setNewOriginalUrl('');
            setCurrentPage(1);
        } catch (err) {
            setError(err.message);
        }
    };

    return (
        <div className="urls-list-page">
            <Header />
            <h1>Shorted Urls</h1>
            {isAuthenticated && (
                <form onSubmit={handleCreateUrl} className="new-url-form">
                    <input
                        className="url-input"
                        type="text"
                        placeholder="Enter Name"
                        value={newUrlName}
                        onChange={(e) => setNewUrlName(e.target.value)}
                        required
                    />
                    <input
                        className="url-input"
                        type="text"
                        placeholder="Enter Original URL"
                        value={newOriginalUrl}
                        onChange={(e) => setNewOriginalUrl(e.target.value)}
                        required
                    />
                    <button type="submit">Create URL</button>
                </form>
            )}
            <table className="urls-table">
                <thead>
                <tr>
                    <td>Name</td>
                    <td>Short Url</td>
                    <td>Original Url</td>
                </tr>
                </thead>
                <tbody>
                {urls.map((url) => (
                    <tr key={url.id} onClick={() => handleRowClick(url.id)}>
                        <td>{url.name}</td>
                        <td><a href={url.shortUrl}>{url.shortUrl}</a></td>
                        <td><a href={url.originalUrl}>{url.originalUrl}</a></td>
                    </tr>
                ))}
                </tbody>
            </table>
            <PaginationCounter
                pagesCount={pagesCount}
                currentPage={currentPage}
                onPageChange={setCurrentPage}
            />
            {error && <p className="error">{error}</p>}
        </div>
    );
};

export default UrlsListPage;