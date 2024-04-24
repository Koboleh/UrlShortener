import {useNavigate, useParams} from "react-router-dom";
import {useEffect, useState} from "react";
import useAuthStore from "../../stores/authStore";
import {urlService} from "../../services/urlService";
import Header from "../../components/Header/Header";
import {format} from "date-fns";
import "./UrlPage.css"


const UrlPage = () => {
    const {id} = useParams();
    const [url, setUrl] = useState('');
    const [error, setError] = useState('');
    const userId = useAuthStore((state) => state.user?.id);
    const userRole = useAuthStore((state) => state.user?.role);
    const isAuthenticated = useAuthStore((state) => state.isAuthenticated);
    const navigate = useNavigate();

    useEffect(() => {

        const fetchUrl = async () => {
            try {
                const url = await urlService.getUrlById(id);
                setUrl(url);
            } catch (err) {
                setError(err)
            }
        }

        if(!isAuthenticated) {
            navigate('/');
        }
        else {
            fetchUrl();
        }

    },[id, isAuthenticated, userId])

    const handleDeleteUrlRecord = async () => {
        try {
            if(isAuthenticated && userId === url.userId) {
                await urlService.deleteUrl(id);
                navigate('/');
            }
        } catch (err) {
            setError(err)
        }
    }

    const formattedDate = url ? format(new Date(url.createdAt), 'yyyy.MM.dd') : '';

    return(
        <div className="url-page">
            <Header/>
            <div className="url-info-container">
                <div className="url-main-info">
                    <p className="url-name">{url.name}</p>
                    <p className="url-user">Created: {formattedDate} by {url.ownerName}</p>
                </div>
                <div className="url-additional-info">
                    <p>Shorted url: <a href={url.shortUrl}>{url.shortUrl}</a></p>
                    <p>Original url: <a href={url.originalUrl}>{url.originalUrl}</a></p>
                </div>
                {isAuthenticated && (userId === url.userId || userRole === 'Admin') && (
                    <button className="url-delete-button" onClick={() => handleDeleteUrlRecord()}>Delete Url</button>
                )}
                {error ?? <div>Error: {error}</div>}
            </div>
        </div>
    )
}

export default UrlPage;