<template>
  <AppLayout>
    <v-container fluid class="pa-6 bg-grey-lighten-4">

      <!-- HEADER -->
      <div class="mb-6">
        <div class="text-h5 font-weight-bold">Company Dashboard</div>
        <div class="text-body-2 text-grey-darken-1">
          Overview of system data
        </div>
      </div>

      <!-- ================= STATS ================= -->
      <v-row dense>
        <v-col
          v-for="card in stats"
          :key="card.title"
          cols="12"
          sm="6"
          md="2"
        >
          <v-card class="pa-4 rounded-xl elevation-2">
            <v-icon color="primary">{{ card.icon }}</v-icon>

            <div class="text-h6 font-weight-bold mt-2">
              {{ card.value }}
            </div>

            <div class="text-caption text-grey">
              {{ card.title }}
            </div>
          </v-card>
        </v-col>
      </v-row>

      <!-- ================= TABLE ================= -->
      <v-card class="rounded-xl elevation-2 mt-6">
        <v-card-title>Tasks</v-card-title>

        <v-table>
          <thead>
            <tr>
              <th>ID</th>
              <th>Status</th>
              <th>User</th>
            </tr>
          </thead>

          <tbody>
            <tr v-for="task in tasks" :key="task.id">
              <td>{{ task.id }}</td>

              <td>
                <v-chip :color="getStatusColor(task.status ?? '')" size="small">
                  {{ task.status }}
                </v-chip>
              </td>

              <td>{{ task.assignedTo ?? 'Unassigned' }}</td>
            </tr>
          </tbody>
        </v-table>
      </v-card>

    </v-container>
  </AppLayout>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import AppLayout from '@/layouts/AppLayout.vue'
import { TaskService } from '@/services/TaskService'
import { adminService } from '@/services/adminService'

const tasks = ref<any[]>([])
const stats = ref<any[]>([])

function getStatusColor(status: string) {
  switch (status) {
    case 'DONE':
      return 'green'
    case 'IN_PROGRESS':
      return 'blue'
    case 'TODO':
      return 'orange'
    default:
      return 'grey'
  }
}

onMounted(async () => {
  const [taskRes, authKpis, projectKpis] = await Promise.all([
    TaskService.list(),
    adminService.getAuthKPIs(),
    adminService.getProjectKPIs(),
  ])

  tasks.value = taskRes

  stats.value = [
    { title: 'Utilisateurs', value: authKpis.totalUsers, icon: 'mdi-account-group' },
    { title: 'Projets', value: projectKpis.totalProjects, icon: 'mdi-folder-multiple' },
    { title: 'Commits', value: projectKpis.totalCommits, icon: 'mdi-source-commit' },
    { title: 'Total Tasks', value: taskRes.length, icon: 'mdi-clipboard' },
    { title: 'TODO', value: taskRes.filter((t: any) => t.status === 'TODO').length, icon: 'mdi-alert' },
    { title: 'DONE', value: taskRes.filter((t: any) => t.status === 'DONE').length, icon: 'mdi-check' },
  ]
})
</script>
