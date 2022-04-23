import axios from 'axios';
import authOidc from './authClient';

const httpClient = axios.create({
    baseURL: 'http://localhost:5000/',
    timeout: 5000
});

httpClient.interceptors.request.use(request => {
    // add auth header with jwt if account is logged in and request is to the api
    const account = authOidc.user;
    const isLoggedIn = authOidc.isAuthenticated;
    const isApiUrl = request.url.startsWith('/api')

    if (isLoggedIn && isApiUrl) {
        request.headers.common.Authorization = `Bearer ${account.access_token}`;
    }

    return request;
})

httpClient.defaults.headers.post['Content-Type'] = 'application/json';
export default httpClient;