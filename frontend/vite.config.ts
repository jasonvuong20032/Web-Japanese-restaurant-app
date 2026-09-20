import { fileURLToPath, URL } from 'node:url'
import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import tailwindcss from '@tailwindcss/vite'

// Backend .NET chạy ở http://localhost:5187 (xem backend/SakuraTei.Api/Properties/launchSettings.json).
// Proxy /api để môi trường dev không phải đụng tới CORS.
const API_TARGET = process.env.VITE_API_TARGET ?? 'http://localhost:5187'

export default defineConfig({
  plugins: [react(), tailwindcss()],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url)),
    },
  },
  server: {
    port: 5173,
    proxy: {
      '/api': {
        target: API_TARGET,
        changeOrigin: true,
      },
    },
  },
  build: {
    outDir: 'dist',
    sourcemap: true,
  },
})
