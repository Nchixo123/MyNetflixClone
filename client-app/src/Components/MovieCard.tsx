import React from 'react';

interface MovieCardProps {
    title: string;
    imageUrl: string;
    description: string;
    onClick: () => void;
}

const MovieCard: React.FC<MovieCardProps> = ({ title, imageUrl, description, onClick }) => {
    return (
        <div className="max-w-sm rounded overflow-hidden shadow-lg cursor-pointer" onClick={onClick}>
            <img className="w-full" src={imageUrl} alt={title} />
            <div className="px-6 py-4">
                <div className="font-bold text-xl mb-2">{title}</div>
                <p className="text-gray-700 text-base">
                    {description}
                </p>
            </div>
        </div>
    );
};

export default MovieCard;
