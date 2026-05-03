# MISSION DE REFACTORING : THE LAST MILE (GitDock Backend)

**CONTEXTE :** Le merge de la gamification et des tâches a généré des doublons sévères. L'objectif est de rétablir une compilation parfaite à 100% sur les environnements Java et .NET. Applique strictement le `.cursorrules`.

**RAPPEL WORKSPACE :** Le dossier `copix/` est la source de vérité pour toute configuration. Le dossier `ya_rebbi_ssalama/` contient le code métier nouveau à intégrer. 
**ACTION SYSTÉMATIQUE :** SUPPRIME TOUTES LES BALISES GIT (`<<<<<<<`, `=======`, `>>>>>>>`) de chaque fichier que tu ouvres.

---

## MISSION 1 : CONFIGURATIONS ET CLASSES GLOBALES (Java & .NET)

**Objectif :** Éliminer les erreurs de build basiques.

### 1.1 — `pom.xml` de gitdock-auth
**Fichier cible :** `ya_rebbi_ssalama/backend/gitdock-backend/gitdock-auth/pom.xml`
**Référence config :** `copix/backend/gitdock-backend/gitdock-auth/pom.xml`
**Actions :**
- Supprimer toutes les balises de conflit Git.
- Ne garder qu'une seule racine `<project>` (utilise la structure de `copix/`).
- Fusionner les `<properties>` en un seul bloc. **En cas de conflit sur une propriété (ex: `<java.version>`), la valeur de `copix/` l'emporte toujours.**
- Fusionner les `<dependencyManagement>` en un seul bloc.
- Dédoublonner la liste des `<dependencies>`.

### 1.2 — `SecurityConfig.java` (Auth)
**Fichier cible :** `ya_rebbi_ssalama/backend/gitdock-backend/gitdock-auth/src/main/java/edu/ehei/gitdock/gitdockauth/security/SecurityConfig.java`
**Actions :**
- Supprimer toutes les balises de conflit Git.
- Supprimer l'annotation `@EnableMethodSecurity` en double — n'en garder qu'une.
- Fusionner les deux blocs `.authorizeHttpRequests()` en un seul.
- S'assurer que les routes suivantes sont en `.permitAll()` :
  - `/api/auth/users/internal/invite`
  - `/actuator/**`
  - `/api/auth/oauth/**`
  - `/api/auth/activate/**`
  - `/api/auth/login`
  - `/api/auth/register`
- Ne garder qu'un seul bean `corsConfigurationSource` retournant une seule `UrlBasedCorsConfigurationSource`.

### 1.3 — `AdminInitializer.java`
**Fichier cible :** `ya_rebbi_ssalama/backend/gitdock-backend/gitdock-auth/src/main/java/edu/ehei/gitdock/gitdockauth/config/AdminInitializer.java`
**Actions :**
- Supprimer toutes les balises de conflit Git.
- Supprimer la déclaration `private static final Logger log = ...` → l'annotation `@Slf4j` suffit.
- Supprimer le constructeur manuel → l'annotation `@RequiredArgsConstructor` suffit.

### 1.4 — `UserAccountRepository.java`
**Fichier cible :** `ya_rebbi_ssalama/backend/gitdock-backend/gitdock-auth/src/main/java/edu/ehei/gitdock/gitdockauth/repository/UserAccountRepository.java`
**Actions :**
- Supprimer toutes les balises de conflit Git.
- Supprimer `findByEmailInAndIsDeletedFalse` en double.
- Supprimer `existsByEmail` en double.
- Pour `findByEmail` : supprimer le conflit entre la méthode `default` et la déclaration classique. Garder uniquement `Optional<UserAccount> findByEmailAndIsDeletedFalse(String email)` comme méthode principale.

### 1.5 — `GithubOAuthStrategyImpl.java`
**Fichier cible :** `ya_rebbi_ssalama/backend/gitdock-backend/gitdock-auth/src/main/java/edu/ehei/gitdock/gitdockauth/strategy/impl/GithubOAuthStrategyImpl.java`
**Actions :**
- Supprimer toutes les balises de conflit Git.
- Supprimer la déclaration de package en double au début du fichier. Ne garder qu'une seule ligne : `package edu.ehei.gitdock.gitdockauth.strategy.impl;`.

### 1.6 — `Program.cs`
**Fichier cible :** `ya_rebbi_ssalama/backend/gitdock-backend/gitdock-gamification/backend/backend/Program.cs`
**Référence config :** `copix/backend/gitdock-backend/gitdock-gamification/backend/backend/appsettings.json`
**Actions :**
- Supprimer toutes les balises de conflit Git.
- Supprimer les injections en double — ne garder que la version avec interface :
  - `AddScoped<IBadgeService, BadgeService>()` (supprimer `AddScoped<BadgeService>()`)
  - `AddScoped<ILevelService, LevelService>()` (supprimer `AddScoped<LevelService>()`)
  - `AddScoped<ITagService, TagService>()` (supprimer `AddScoped<TagService>()`)
  - `AddScoped<IXpConfigService, XpConfigService>()` (supprimer `AddScoped<XpConfigService>()`)
- Ne garder qu'une seule configuration CORS nommée "AllowVueApp".
- Ne garder qu'un seul appel `.AddOpenApi()`.

**Rapport à générer :** `ya_rebbi_ssalama/docs/LAST_MILE_MISSION_1_REPORT.md`

---

## MISSION 2 : OPÉRATION CHIRURGICALE SUR USER_ACCOUNT_SERVICE_IMPL

**Fichier cible unique :** `ya_rebbi_ssalama/backend/gitdock-backend/gitdock-auth/src/main/java/edu/ehei/gitdock/gitdockauth/service/UserAccountServiceImpl.java`

**RÈGLE GÉNÉRALE :** Supprimer ABSOLUMENT TOUTES les balises Git (`<<<<<<<`, `=======`, `>>>>>>>`) du fichier en premier lieu.

### 2.1 — Imports
- Supprimer `import org.springframework.amqp.rabbit.core.RabbitTemplate;` en double.
- Résoudre le conflit entre `import java.util.*;` et `import java.util.List;` — garder uniquement `import java.util.*;`.
- Supprimer tout import en double.

### 2.2 — Méthodes GET
- Dédoublonner `getUsersSummaries` — garder l'implémentation complète.
- Dédoublonner `getUserByEmail` — garder l'implémentation qui utilise `findByEmailAndIsDeletedFalse`.
- Supprimer toute signature orpheline sans corps de méthode.

### 2.3 — Méthode `inviteUserFromProject`
- Fusionner les deux blocs en un seul.
- Une seule déclaration de la variable `existingUserOpt`.
- La logique finale doit :
  1. Vérifier l'utilisateur existant (Scénario A).
  2. Créer le nouveau avec workspace personnel si inexistant (Scénario B).
  3. Envoyer un événement RabbitMQ `TYPE_PROJECT_INVITATION` sur exchange `gitdock.exchange` avec routing key `notification.routing.key`.

### 2.4 — Méthodes CRUD (`updateUser`, `softDeleteUser`, `restoreUser`, `deleteUser`)
- Supprimer les déclarations de méthodes imbriquées ou répétées.
- Dans `updateUser` : une seule déclaration de la variable locale (choisir le nom existant, ex: `userToUpdate` ou `user`, sans dupliquer la déclaration).
- Dans `deleteUser` : supprimer tout code mort après le `throw` sur la protection `GHOST_EMAIL`.
- Dans `softDeleteUser` : supprimer la déclaration dupliquée de la variable locale.
- Vérifier que les variables appelées pointent bien vers la propriété injectée `userRepository` (de type `UserAccountRepository`).

### 2.5 — Méthode `mapToSummary`
- Il y a deux méthodes `mapToSummary` à la fin du fichier.
- Supprimer la deuxième — celle qui contient un bloc `stream().map(...)` dans un constructeur d'objet.
- Ne garder que la version propre : `return UserSummaryDTO.builder()...build();`

**Rapport à générer :** `ya_rebbi_ssalama/docs/LAST_MILE_MISSION_2_REPORT.md`

---

## MISSION 3 : PURGE DES CONFLITS GAMIFICATION (.NET)

**Répertoire de base :** `ya_rebbi_ssalama/backend/gitdock-backend/gitdock-gamification/backend/backend/`
**RÈGLE GÉNÉRALE :** Supprimer TOUTES les balises Git dans les fichiers modifiés.

### 3.1 — `LevelService.cs`
**Fichier cible :** `Services/LevelService.cs`
**Actions :**
- Modifier la signature en `public class LevelService : ILevelService`.

### 3.2 — `TagService.cs`
**Fichier cible :** `Services/TagService.cs`
**Actions :**
- Supprimer le bloc `namespace` en double.
- Supprimer les `using` en double.
- La classe finale doit implémenter `ITagService` et injecter `ITagRepository`.

### 3.3 — `BadgeService.cs`
**Fichier cible :** `Services/BadgeService.cs`
**Actions :**
- La classe entière est dupliquée — l'une injecte `ApplicationDbContext`, l'autre `IBadgeRepository`. Supprime intégralement la version qui injecte `ApplicationDbContext`. 
- Ne conserver QUE : `public class BadgeService : IBadgeService` qui utilise les repositories.

### 3.4 — `BadgesController.cs` et `TagsController.cs`
**Fichiers cibles :** `Controllers/BadgesController.cs` & `Controllers/TagsController.cs`
**Actions :**
- Supprimer les constructeurs qui injectent les classes concrètes (ex: `BadgeService`). Ne garder QUE les constructeurs qui injectent les interfaces (ex: `IBadgeService`).
- Supprimer les déclarations en double des champs de classe et des annotations `[Route]`.

**Rapport à générer :** `ya_rebbi_ssalama/docs/LAST_MILE_MISSION_3_REPORT.md`

---

## MISSION 4 : VÉRIFICATION D'INTÉGRITÉ DES MODÈLES ET CONTRÔLEURS

**RÈGLE GÉNÉRALE :** Supprimer TOUTES les balises Git dans les fichiers modifiés.

### 4.1 — `Project.java`
**Fichier cible :** `ya_rebbi_ssalama/backend/gitdock-backend/gitdock-project/src/main/java/edu/ehei/gitdock/gitdockproject/model/Project.java`
**Actions :**
- Supprimer la deuxième annotation `@Table`.
- Supprimer la déclaration orpheline du champ `status` (celle annotée `@Enumerated` par erreur sur une `List<Part>`). Garder uniquement `private ProjectStatus status;` à sa place correcte.
- Supprimer les méthodes `onCreate()` et `onUpdate()` en double — garder la version `protected`.

### 4.2 — `UserProject.java`
**Fichier cible :** `ya_rebbi_ssalama/backend/gitdock-backend/gitdock-project/src/main/java/edu/ehei/gitdock/gitdockproject/model/UserProject.java`
**Actions :**
- Supprimer le champ `private LocalDateTime assignedAt;` non annoté.
- Garder uniquement la version annotée `@Column(name = "created_at"...)` gérée par `@PrePersist`.

### 4.3 — `UserAccountController.java`
**Fichier cible :** `ya_rebbi_ssalama/backend/gitdock-backend/gitdock-auth/src/main/java/edu/ehei/gitdock/gitdockauth/controller/UserAccountController.java`
**Actions :**
- Supprimer l'annotation `@Slf4j` en double.
- Pour chaque endpoint dupliqué, garder la signature la plus complète pour éviter les conflits `Ambiguous handler methods`:
  - `/summaries` → `@RequestParam(value = "ids", required = false) List<Long> ids`
  - `/by-email` → `@RequestParam("email") String email`
  - `inviteUser` → version avec le commentaire métier
  - `updateUser` → version avec `@PathVariable` et `@RequestBody` séparés
  - `softDeleteUser` → `@PutMapping("/{id}/soft-delete")`
  - `getUsersByEmails` → `@PostMapping("/internal/by-emails")`

### 4.4 — `JwtFilter.java`
**Fichier cible :** `ya_rebbi_ssalama/backend/gitdock-backend/gitdock-auth/src/main/java/edu/ehei/gitdock/gitdockauth/security/JwtFilter.java`
**Actions :**
- Supprimer la double extraction JWT dans le bloc `try`.
- Ne garder qu'une seule variable `final String jwt` déclarée avant le bloc `try`.
- Fusionner les vérifications anti-bug (`jwt.isBlank()` et `"null".equals(jwt)`) en un seul bloc `if`.

**Rapport à générer :** `ya_rebbi_ssalama/docs/LAST_MILE_MISSION_4_REPORT.md`