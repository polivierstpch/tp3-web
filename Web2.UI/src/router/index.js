import authOidc from '../services/authClient';
import { createRouter, createWebHistory } from 'vue-router';
import HomeView from '../views/HomeView.vue';

const routes = [
  {
    path: '/',
    name: 'home',
    component: HomeView
  },
  {
    path: '/evenements',
    name: 'evenements',
    component: () => import('../views/EventsView.vue'),
    children: [
      {
        path: ':id', 
        component: () => import('../views/evenements/EventDetailsView.vue'),
      },
      {
        path: ':id/participation',
        component: () => import('../views/ParticipationsView.vue')
      }
    ]
  },
  {
    path: '/profil',
    name: 'profil',
    component: () => import('../views/ProfileView.vue'),
    meta: {
      authName: authOidc.authName
    }
  }
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
});

authOidc.useRouter(router)

export default router;
