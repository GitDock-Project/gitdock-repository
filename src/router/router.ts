import { createRouter, createWebHistory } from "vue-router";
import type { RouteRecordRaw } from "vue-router";
import DashboardTask from "../views/DashboardTask.vue";

const routes: Array<RouteRecordRaw> = [
  { path: "/", redirect: "/dashboardtask" }, // redirection vers la seule page
  { path: "/dashboardtask", component: DashboardTask }
];

export const router = createRouter({
  history: createWebHistory(),
  routes
});
