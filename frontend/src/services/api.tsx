import axios from "axios"

const api = axios.create({
    baseURL: "https://localhost:7001",
});

api.interceptors.request.use(async (config) => {
    const token = 'seu-token-aqui';
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config; 
}, async(error)=> {
    return Promise.reject(error); 
});

export default api; 