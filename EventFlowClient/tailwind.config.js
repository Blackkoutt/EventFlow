/** @type {import('tailwindcss').Config} */
export default {
  content: ["./index.html", "./src/**/*.{js,ts,jsx,tsx}"],
  theme: {
    extend: {
      colors: {
        primaryPurple: "#7B2CBF",
        secondaryPurple: "#987EFE",
      },
    },
  },
  plugins: [],
};
