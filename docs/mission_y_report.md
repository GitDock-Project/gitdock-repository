# Rapport de mission — Phase 2 (Dashboard Entreprise)

**Périmètre :** `ya_rebbi_ssalama/frontend/gitdock-frontend`  
**Référence fonctionnelle :** `INSTRUCTIONS_FRONTEND.md` — Phase 2  
**Date :** 4 mai 2026

---

## Synthèse

Intégration stricte du **dashboard entreprise** : vue `DashboardCompany.vue` alignée sur `AppLayout`, services `TaskService` et `adminService`, route racine `/company/dashboard` avec meta `requiresCompanyAdmin`, garde de navigation, lien sidebar « Espace Entreprise ».

**Build :** `npm run build` (Vite) exécuté avec succès après les changements.

---

## Fichier source analysé

Le fichier mentionné dans les instructions est `frontend/front_total/my-project/src/views/dashbord/DashboadCompany.vue`. Dans ce dépôt, l’équivalent se trouve sous :

`ya_rebbi_ssalama/frontend/front_total/my-project/src/views/dashbord/DashboadCompany.vue`

(contenu : `v-app`, `v-navigation-drawer`, `v-app-bar`, `v-main` > `v-container`, stats + table des tâches, import `task.service` et `getAllTasks()`.)

---

## 2.1 à 2.3 — `src/views/dashboard/DashboardCompany.vue`

### État initial

Le fichier de destination contenait une **corruption** (concaténation avec un autre composant / blocs Markdown après la balise `</style>`). Il a été **entièrement réécrit** pour ne garder que le dashboard entreprise conforme aux instructions.

### Transformations appliquées

| Exigence | Réalisation |
|----------|-------------|
| Suppression du layout autonome | Retrait de `v-app`, `v-navigation-drawer`, `v-app-bar`, `v-main` et de la liste de navigation factice du prototype. |
| `AppLayout` | Le contenu utile du prototype (équivalent à l’intérieur de `<v-container>`) est enveloppé dans `<AppLayout>`. |
| Services | `TaskService.list()` et `adminService.getAuthKPIs()` / `getProjectKPIs()` via `Promise.all`, comme dans les instructions. |
| KPIs | Tableau `stats` à 6 entrées : Utilisateurs, Projets, Commits, Total Tasks, TODO, DONE (même structure que le snippet `INSTRUCTIONS_FRONTEND.md`). |
| Table des tâches | Conservée (ID, statut avec `v-chip`, colonne utilisateur / `assignedTo`). |
| `getStatusColor` | Conservé pour la cohérence avec les statuts `TODO` / `IN_PROGRESS` / `DONE`. |
| Styles scoped du prototype | Supprimés (`.sidebar`, `.appbar`, `.search` ne s’appliquaient qu’au layout retiré). |

**Imports :** `AppLayout`, `{ TaskService }` depuis `@/services/TaskService`, `{ adminService }` depuis `@/services/adminService`.

---

## 2.4 — `src/router/index.ts`

- Import : `DashboardCompany` depuis `@/views/dashboard/DashboardCompany.vue`.
- Nouvelle route **au niveau racine** (hors enfants de `/dashboard`) :

```text
path: '/company/dashboard'
name: 'company-dashboard'
component: DashboardCompany
meta: { requiresAuth: true, requiresCompanyAdmin: true }
```

- Extension du module augmenté `vue-router` : propriété optionnelle `requiresCompanyAdmin?: boolean` dans `RouteMeta`.

---

## 2.5 — `src/router/guards.ts`

Ajout, **immédiatement après** le bloc `requiresSuperAdmin`, du contrôle demandé : si `to.meta.requiresCompanyAdmin` est vrai et que le rôle n’est ni `ROLE_COMPANY_ADMIN` ni `ROLE_SUPER_ADMIN`, redirection vers `/projects`.

La numérotation des commentaires existants (« 3. Route guest ») est inchangée après ce bloc.

---

## 2.6 — `src/components/navigation/AppSideBar.vue`

- Bloc `<li v-if="isCompanyAdmin">` avec `router-link` vers `/company/dashboard`, libellé « Espace Entreprise », classes et `active-class` conformes au snippet des instructions.
- Icône SVG : attribut `d` normalisé sur une seule ligne (équivalent au chemin Heroicons / bâtiment des instructions, sans saut de ligne au milieu de `d`).
- `computed` `isCompanyAdmin` : `ROLE_COMPANY_ADMIN` **ou** `ROLE_SUPER_ADMIN`.

**Note Windows :** le projet importe parfois `@/components/navigation/AppSidebar.vue` (casse différente) ; sous ce système de fichiers, il s’agit du même fichier que `AppSideBar.vue`.

---

## Liste des fichiers modifiés ou créés

| Chemin | Action |
|--------|--------|
| `ya_rebbi_ssalama/frontend/gitdock-frontend/src/views/dashboard/DashboardCompany.vue` | Réécriture complète (correction corruption + Phase 2) |
| `ya_rebbi_ssalama/frontend/gitdock-frontend/src/router/index.ts` | Route + import + `RouteMeta` |
| `ya_rebbi_ssalama/frontend/gitdock-frontend/src/router/guards.ts` | Garde `requiresCompanyAdmin` |
| `ya_rebbi_ssalama/frontend/gitdock-frontend/src/components/navigation/AppSideBar.vue` | Lien + `isCompanyAdmin` |

---

## Fichier de rapport

- `docs/mission_y_report.md` (ce document)

---

## Points d’attention runtime

- Les endpoints `adminService` (`/auth/admin/kpis`, `/projects/admin/kpis`) et `TaskService.list()` (`/tasks`) doivent être autorisés pour les rôles **company admin** / **super admin** côté gateway, sinon la vue affichera des erreurs réseau à l’`onMounted` (comportement attendu si le backend restreint l’accès).
