# LAST MILE — Mission 5 : test du monolithe (build parent Maven)

## Objectif

Compiler l’écosystème Java **en une seule passe** depuis le POM parent  
`ya_rebbi_ssalama/backend/gitdock-backend/` avec :

`mvn clean compile -DskipTests`

## Périmètre du reactor

Modules Maven listés dans le parent (aucun `gitdock-task` / PHP — hors reactor, comme attendu) :

| Module | Rôle |
|--------|------|
| `gitdock-discovery` | Service discovery |
| `gitdock-gateway` | API Gateway |
| `gitdock-auth` | Authentification |
| `gitdock-project` | Projets / collaboration |
| `gitdock-sync` | Synchronisation |
| `gitdock-notification` | Notifications |

## Premier essai : échec sur `gitdock-project`

La commande a d’abord échoué sur **`gitdock-project`** : fusion partielle dans **`ProjectRepository.java`** :

- Méthodes **`findByManagedById`** et **`findByUrlContainingIgnoreCase`** déclarées **deux fois**.
- Bloc erroné : `@Query` sur `userId` rattaché à une méthode nommée `findByManagedById` (incohérence nom / JPQL), en plus de la vraie méthode dérivée `findByManagedById`.
- Import redondant `org.springframework.data.jpa.repository.*`.

## Correctif appliqué

**Fichier aligné sur `copix/`** (référence) :

`ya_rebbi_ssalama/backend/gitdock-backend/gitdock-project/src/main/java/edu/ehei/gitdock/gitdockproject/repository/ProjectRepository.java`

- Une seule déclaration de **`findAssignedProjects`**, **`findByManagedById`**, **`findByUrlContainingIgnoreCase`**.
- Imports nettoyés (suppression du wildcard JPA en double).

Les nombreuses erreurs « cannot find symbol » (getters Lombok, builders, etc.) étaient des **effets dominos** : le compilateur ne pouvait pas valider correctement le module tant que l’interface repository était invalide.

## Second essai : succès

Commande :

```text
cd ya_rebbi_ssalama/backend/gitdock-backend
mvn clean compile -DskipTests
```

**Résultat : `BUILD SUCCESS`** (tous les modules **SUCCESS**, temps total ~22 s sur l’environnement de build).

### Reactor Summary (extrait)

| Module | Statut |
|--------|--------|
| gitdock-backend (pom) | SUCCESS |
| gitdock-discovery | SUCCESS |
| gitdock-gateway | SUCCESS |
| gitdock-auth | SUCCESS |
| gitdock-project | SUCCESS |
| gitdock-sync | SUCCESS |
| gitdock-notification | SUCCESS |

## `gitdock-task` (PHP / Symfony)

Non inclus dans ce parent Maven : **aucune exclusion** n’a été nécessaire ; le build global Java n’interagit pas avec ce module.

## Fichiers modifiés (Mission 5)

| Fichier |
|---------|
| `ya_rebbi_ssalama/backend/gitdock-backend/gitdock-project/src/main/java/edu/ehei/gitdock/gitdockproject/repository/ProjectRepository.java` |

## Balises Git

Aucune balise `<<<<<<<` / `=======` / `>>>>>>>` dans le fichier corrigé ; dommage de type **duplication de signatures** (merge partiel).

---

**Conclusion :** le parent **`gitdock-backend`** compile l’intégralité des microservices Java du workspace cible après dédoublonnage du **`ProjectRepository`** sur le modèle **copix**.
