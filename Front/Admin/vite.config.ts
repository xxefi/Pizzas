import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import * as fs from "fs";
import tailwindcss from "@tailwindcss/vite";

export default defineConfig({
  plugins: [react(), tailwindcss()],
  build: {
    outDir: "dist",
  },
  server: {
    https: {
      key: fs.readFileSync("192.168.2.159+2-key.pem"),
      cert: fs.readFileSync("192.168.2.159+2.pem"),
    },
    host: "localhost",
    port: 3001,
  },
});
