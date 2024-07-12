// src/Components/SearchBar.tsx

import React, { useState } from 'react';

interface SearchBarProps {
    onSearch: (keyword: string) => void;
}

const SearchBar: React.FC<SearchBarProps> = ({ onSearch }) => {
    const [keyword, setKeyword] = useState('');

    const handleSearch = (e: React.FormEvent) => {
        e.preventDefault();
        onSearch(keyword);
    };

    return (
        <form onSubmit={handleSearch} className="w-full max-w-md mx-auto flex items-center">
            <input
                type="text"
                value={keyword}
                onChange={(e) => setKeyword(e.target.value)}
                className="flex-grow px-4 py-2 border rounded-l"
                placeholder="Search movies..."
            />
            <button type="submit" className="bg-[#002379] text-white px-4 py-2 rounded-r">
                Search
            </button>
        </form>
    );
};

export default SearchBar;
