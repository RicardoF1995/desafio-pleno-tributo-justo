import axios from "axios";

const instance = axios.create({
  baseURL: "http://localhost:5240/api", // Altere para a porta correta do seu backend
});

instance.interceptors.request.use((config) => {
  const token = localStorage.getItem("token");
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export default instance;
