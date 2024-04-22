import { createRouter, createWebHistory } from 'vue-router'
import App from "@/App.vue";
import CreatePatient from "@/components/CreatePatient.vue";
import DeletePatient from "@/components/DeletePatient.vue";
import ViewMeasurements from "@/components/ViewMeasurements.vue";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      redirect: '/measurements',
      children: [
        {
          path: '/create',
          name: 'create',
          component: CreatePatient
        },
        {
          path: '/delete',
          name: 'delete',
          component: DeletePatient
        },
        {
            path: '/measurements',
            name: 'measurements',
            component: ViewMeasurements
        }
      ]
    }
  ]
})

export default router
