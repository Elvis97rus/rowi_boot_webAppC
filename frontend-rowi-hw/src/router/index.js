import { createRouter, createWebHistory } from "vue-router";
import HomeView from "../views/HomeView.vue";
import VisitsView from "../views/VisitsView.vue";
import PatientsView from "../views/PatientsView.vue";
import DoctorsView from "../views/DoctorsView.vue";
import MedicinesView from "../views/MedicinesView.vue";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      name: "home",
      component: HomeView,
    },
    {
      path: "/visits",
      name: "visits",
      component: VisitsView,
    },
    {
      path: "/visits/:id",
      name: "single-visit",
      component: VisitsView,
    },
    {
      path: "/patients",
      name: "patients",
      component: PatientsView,
    },
    {
      path: "/patients/:id",
      name: "single-patient",
      component: PatientsView,
    },
    {
      path: "/medicines",
      name: "medicines",
      component: MedicinesView,
    },
    {
      path: "/medicines/:id",
      name: "single-medicine",
      component: MedicinesView,
    },
    {
      path: "/doctors",
      name: "doctors",
      component: DoctorsView,
    },
    {
      path: "/doctors/:id",
      name: "single-doctors",
      component: DoctorsView,
    },
  ],
});

export default router;
