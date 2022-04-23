import { createApp } from 'vue'
import App from './App.vue'
import authOidc from './services/authClient';
import router from './router'
import store from './store'


authOidc.startup()
    .then(ok => {
        if (ok) {
            const app = createApp(App);
            app.config.globalProperties.$oidc = authOidc;
            app.use(store).use(router)
            .mount('#app');
        }
    });



