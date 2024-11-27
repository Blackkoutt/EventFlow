import { defineConfig } from "vite";
import fs from "fs";
import react from "@vitejs/plugin-react-swc";
import mkcert from "vite-plugin-mkcert";
import basicSsl from "@vitejs/plugin-basic-ssl";
import { createServer } from "https";

// https://vite.dev/config/

//const hmr = new WebSocket("wss://localhost:5173/", "vite-hmr");
export default defineConfig({
  /* server: {
    host: "0.0.0.0",
    port: 5173,
    proxy: {
      "/socket.io": {
        target: "ws://local.domain.com:3004",
        ws: true,
      },
    },
    hmr: {
      path: "/ws",
      port: 3004,
    },
    /*https: {
      key: fs.readFileSync("./key.pem"),
      cert: fs.readFileSync("./cert.pem"),
    },
  },*/
  plugins: [react()],
});
