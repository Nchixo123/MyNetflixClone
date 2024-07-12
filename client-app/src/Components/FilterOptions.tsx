import React, { useState } from 'react';

interface FilterOptionsProps {
    onFilter: (genre: string, minRating: number) => void;
}

const FilterOptions: React.FC<FilterOptionsProps> = ({ onFilter }) => {
    const [genre, setGenre] = useState('');
    const [minRating, setMinRating] = useState<number | undefined>(undefined);

    const handleFilter = (e: React.FormEvent) => {
        e.preventDefault();
        onFilter(genre, minRating || 0);
    };

    return (
        <form onSubmit={handleFilter} className="flex flex-col">
            <label htmlFor="genre" className="block text-gray-700 mb-2">Genre</label>
            <select
                id="genre"
                value={genre}
                onChange={(e) => setGenre(e.target.value)}
                className="mb-4 px-4 py-2 border rounded"
            >
                <option value="">Select Genre</option>
                <option value="Action">Action</option>
                <option value="Comedy">Comedy</option>
                <option value="Drama">Drama</option>
                <option value="Horror">Horror</option>
                <option value="Romance">Romance</option>
                <option value="Sci-Fi">Sci-Fi</option>
                <option value="Thriller">Thriller</option>
                {/* Add more genres as needed */}
            </select>

            <label htmlFor="minRating" className="block text-gray-700 mb-2">Min Rating</label>
            <input
                type="number"
                id="minRating"
                value={minRating ?? ''}
                onChange={(e) => setMinRating(parseInt(e.target.value))}
                className="mb-4 px-4 py-2 border rounded"
                placeholder="Min Rating"
            />

            <button type="submit" className="bg-[#002379] text-white px-4 py-2 rounded">
                Filter
            </button>
        </form>
    );
};

export default FilterOptions;
