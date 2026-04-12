import { createRouter, createWebHistory } from "vue-router";
import type { RouteRecordRaw } from "vue-router";

import DashboardTask from "../views/DashboardTask.vue";
import AddTask from "../views/AddTask.vue";
import UpdateTask from "../views/UpdateTask.vue";

const routes: Array<RouteRecordRaw> = [
  { path: "/", redirect: "/dashboardtask" },

  { path: "/dashboardtask", component: DashboardTask },

  { path: "/addtask", component: AddTask },

  {
    path: "/tasks/edit/:id",
    name: "EditTask",
    component: UpdateTask
  },

  // Nouvelle route pour DeleteTask
  {
    path: "/tasks/delete/:id",
    name: "DeleteTask",
    component: () => import("../views/DeleteTask.vue")
  }
];

export const router = createRouter({
  history: createWebHistory(),
  routes
});