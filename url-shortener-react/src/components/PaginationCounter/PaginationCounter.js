import React from 'react';
import './PaginationCounter.css';

const PaginationCounter = ({ pagesCount, currentPage, onPageChange }) => {
    return (
        <div className="pagination-counter">
            {[...Array(pagesCount).keys()].map(pageNumber => (
                <button
                    key={pageNumber + 1}
                    onClick={() => onPageChange(pageNumber + 1)}
                    className={currentPage === pageNumber + 1 ? 'active' : ''}
                >
                    {pageNumber + 1}
                </button>
            ))}
        </div>
    );
};

export default PaginationCounter;