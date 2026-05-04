# Mission : Stabilisation et Intégration GitDock Frontend

**Dossier de travail exclusif :** `ya_rebbi_ssalama/frontend/gitdock-frontend`

> ⚠️ Ne pas lire ni modifier les dossiers `frontend/front_total/`, `frontend/gitdock-frontend-gamification/` ou tout autre frontend legacy.

---

## Phase 1 : Corrections Critiques (Audit)
Appliquer les corrections suivantes sans altérer le reste de l'application.

### 1. `src/components/gamification/dashboard/user/BadgesGallery.vue`
* Remplacer l'appel fantôme `userBadgeService.getByUserId(userId.value)` par `userBadgeService.getMyBadges()`.
* Remplacer `badgeService.getAll()` par `userBadgeService.getAllAvailableBadges()`.
* Supprimer la logique liée à `userId` devenue inutile après ces corrections.

### 2. `src/views/gamification/DashboardView.vue`
* Corriger l'accès aux rôles Pinia : remplacer `authStore.user?.roles` par `authStore.role`.
* Adapter le computed `isManager` :
  ```typescript
  const isManager = computed(() =>
    authStore.role === 'ROLE_MANAGER' ||
    authStore.role === 'ROLE_SUPER_ADMIN' ||
    authStore.role === 'ROLE_COMPANY_ADMIN'
  )
  ```

### 3. `src/layouts/RouterAppLayout.vue`
Importer et ajouter `<GitDockAiChat />` juste avant la fermeture de la `<div>` principale, comme c'est déjà le cas dans `AppLayout.vue`.
```vue
<script setup>
import GitDockAiChat from '@/components/common/GitDockAiChat.vue'
// ...
</script>
```

### 4. `src/components/common/GitDockAiChat.vue`
Supprimer les 5 lignes orphelines à la fin de la fonction `executeSaga`, après le bloc `finally`. Ces lignes référencent une variable `text` hors scope (`ReferenceError`) et sont un artefact de copier-coller. Ne pas les déplacer — simplement les supprimer.

---

## Phase 2 : Intégration du Dashboard Entreprise
Le fichier source de référence est : `frontend/front_total/my-project/src/views/dashbord/DashboadCompany.vue`.
Le fichier de destination à créer/modifier est : `src/views/dashboard/DashboardCompany.vue`.

### Transformations à appliquer sur `DashboardCompany.vue`

#### 2.1 — Supprimer le layout autonome
Le fichier source contient `<v-navigation-drawer>` et `<v-app-bar>`. Ces éléments DOIVENT être supprimés entièrement.
Envelopper le contenu de `<v-main>` dans `<AppLayout>` :
```vue
<template>
  <AppLayout>
    <!-- contenu du <v-container> existant uniquement -->
  </AppLayout>
</template>

<script setup lang="ts">
import AppLayout from '@/layouts/AppLayout.vue'
// ...
</script>
```

#### 2.2 — Corriger le service importé
Remplacer :

```typescript
import taskService from "@/services/task.service"
```

Par :

```typescript
import { TaskService } from '@/services/TaskService'
```

Adapter l'appel dans `onMounted` :
```typescript
// Avant
const res = await taskService.getAllTasks()
// Après
const res = await TaskService.list()
```

#### 2.3 — Enrichir les KPIs avec adminService
Ajouter les KPIs d'entreprise (utilisateurs, projets, commits) en important `adminService` depuis `@/services/adminService` :
```typescript
import { adminService } from '@/services/adminService'

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
```

#### 2.4 — Mise à jour du Routeur (`src/router/index.ts`)
Ajouter la route suivante dans le tableau `routes`, au niveau racine (pas dans les enfants de `/dashboard`) :
```typescript
import DashboardCompany from '@/views/dashboard/DashboardCompany.vue'

// Dans le tableau routes :
{
  path: '/company/dashboard',
  name: 'company-dashboard',
  component: DashboardCompany,
  meta: { requiresAuth: true, requiresCompanyAdmin: true },
},
```

Ajouter également dans le bloc `declare module 'vue-router'` :

```typescript
requiresCompanyAdmin?: boolean
```

#### 2.5 — Sécurisation (`src/router/guards.ts`)
Ajouter ce bloc dans `router.beforeEach`, après le bloc `requiresSuperAdmin` existant :
```typescript
// Route Company Admin → vérifier le rôle
if (
  to.meta.requiresCompanyAdmin &&
  authStore.role !== 'ROLE_COMPANY_ADMIN' &&
  authStore.role !== 'ROLE_SUPER_ADMIN'
) {
  return next('/projects')
}
```

#### 2.6 — Sidebar (`src/components/navigation/AppSideBar.vue`)
Ajouter un lien visible uniquement par `ROLE_COMPANY_ADMIN` et `ROLE_SUPER_ADMIN`, après le lien "Gamification" existant :
```vue
<li v-if="isCompanyAdmin">
  <router-link
    to="/company/dashboard"
    class="flex items-center px-4 py-2 text-sm font-medium text-gray-700 rounded-md hover:bg-gray-100 transition-colors"
    active-class="bg-blue-50 text-blue-700"
  >
    <svg class="w-5 h-5 mr-3" fill="none" stroke="currentColor" viewBox="0 0 24 24">
      <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
            d="M19 21V5a2 2 0 00-2-2H7a2 2 0 00-2 2v16m14 0h2m-2 0h-5m-9 0H3m2 0h5 
               M9 7h1m-1 4h1m4-4h1m-1 4h1m-2 10v-5a1 1 0 011-1h2a1 1 0 011 1v5m-4 0h4" />
    </svg>
    Espace Entreprise
  </router-link>
</li>
```

Dans le `<script setup>` de la sidebar, ajouter :
```typescript
const isCompanyAdmin = computed(
  () => authStore.role === 'ROLE_COMPANY_ADMIN' || authStore.role === 'ROLE_SUPER_ADMIN'
)
```

---

## Phase 3 : Nettoyage du code mort
Supprimer les fichiers suivants qui ne sont référencés nulle part dans le router :

* `src/views/gamification/GamificationDashboard.vue`
* `src/views/gamification/BadgeGallery.vue`
* `src/components/gamification/dashboard/manager/XpConfigSection.vue`
```