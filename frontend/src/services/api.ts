import axios from "axios";

const api = axios.create({
  baseURL: "http://13.235.56.118:5000/api",
  headers: {
    "Content-Type": "application/json",
  },
});

export default api;