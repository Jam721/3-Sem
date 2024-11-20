import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";

export default defineConfig({
  plugins: [react()],
  server: {
    host: "0.0.0.0", // Указывает, что сервер будет доступен по всем IP-адресам
    port: 5173, // Устанавливает порт на 5173 (по умолчанию для Vite)
  },
});
