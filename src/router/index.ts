import { createRouter, createWebHistory } from '@ionic/vue-router';
import { RouteRecordRaw } from 'vue-router';
import StartPage from '../pages/StartPage.vue'

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    redirect: '/start'
  },
  {
    path: '/start',
    name: 'StartPage',
    component: StartPage
  },
  {
    path: '/login',
    component: () => import('../pages/LoginPage.vue')
  },
  {
    path: '/register',
    component: () => import('../pages/RegisterPage.vue')
  },
  {
    path: '/catalog',
    component: () => import('../pages/CatalogPage.vue')
  },
  {
    path: '/ressource/:id',
    component: () => import('../pages/RessourcesDetailsPage.vue')
  },
  {
    path: '/create',
    component: () => import('../pages/CreateRessource.vue')
  },
  {
    path: '/forgot',
    component: () => import('../pages/ForgotPassword.vue')
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
