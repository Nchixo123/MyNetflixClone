/** @type {import('tailwindcss').Config} */
export default {
  purge: ['./src/**/*.{js,jsx,ts,tsx}', './public/index.html'],
  darkMode: false, // or 'media' or 'class',
  content: [],
  theme: {
      extend: {
          colors: {
              'beige': '#FFFAE6',
              'light-orange': '#FF9F66',
              'orange': '#FF5F00',
              'navy': '#002379',
          },
      },
  },
  plugins: [],
}

