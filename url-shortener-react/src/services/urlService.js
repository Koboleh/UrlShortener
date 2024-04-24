import { axiosInstance } from '../config/apiConfig';


const getUrls = async (paginationParams) => {
    try {
        const response = await axiosInstance.get(`/urls?${new URLSearchParams(paginationParams).toString()}`);
        return response.data;
    } catch (error) {
        throw new Error('Failed to fetch urlsList');
    }
};

const getUrlById = async (id) => {
    try {
        const response = await axiosInstance.get(`/urls/${id}`);
        return response.data;
    } catch (error) {
        console.error(error);
        throw new Error('Failed to fetch url');
    }
};

const createUrl = async (urlData) => {
    try {
        const response = await axiosInstance.post('/urls', urlData);
        return response.data;
    } catch (error) {
        console.error(error);
        throw new Error('Failed to create url');
    }
};

const deleteUrl = async (id) => {
    try {
        const response = await axiosInstance.delete(`/urls/${id}`);
        return response.data;
    } catch (error) {
        console.error(error);
        throw new Error('Failed to delete url');
    }
};

export const urlService = {
    getUrls,
    getUrlById,
    createUrl,
    deleteUrl
};