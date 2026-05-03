# MISSION DE REFACTORING : GITDOCK-GAMIFICATION (.NET CORE)

**CONTEXTE :** Ce service passe d'un monolithe isolé à un microservice Event-Driven. Il gère l'XP, les Niveaux et les Badges. Il ne gère PAS d'utilisateurs réels, mais maintient des profils de progression (`UserGamificationProfile`). Il réagit aux événements de `project` et `task`. Réfère-toi toujours au fichier `.cursorrules` avant de coder.

---

## MISSION 0 : PRÉPARATION DE L'ENVIRONNEMENT
**Objectif :** Initialiser la structure de documentation.
1. Créer le dossier `docs/` à la racine.
2. Créer un fichier `docs/INITIAL_STATE.md` décrivant l'état actuel (monolithique) avant le début des travaux.

---

## MISSION 1 : PURGE & ISOLATION (Le domaine de Gamification)
**Objectif :** Détruire les entités corrompues et mettre en place les profils de progression.
1. Dans le dossier `Domain/`, s'il existe une entité `User.cs` complète, supprime-la.
2. **Créer** l'entité `UserGamificationProfile.cs` :
   - Propriétés : `UserId` (long, PK, clé externe logique), `Xp` (int), `LevelId` (int, FK vers Level).
3. **Créer** l'entité `UserTagProgress.cs` :
   - Propriétés : `Id`, `UserId` (long), `Tag` (string), `Count` (int).
4. Nettoyer le `ApplicationDbContext.cs` pour refléter ces changements (supprimer les anciens `DbSet<User>` et ajouter les nouveaux).
5. **Supprimer** le dossier `Migrations/` complet. L'architecture change tellement qu'il vaut mieux repartir sur une base propre (EF Core générera une nouvelle migration initiale plus tard).

---

## MISSION 2 : COUCHE HTTP (Clients Synchrones)
**Objectif :** Permettre à Gamification de récupérer les noms des utilisateurs pour le Leaderboard.
1. **Créer** un dossier `Clients/`.
2. **Implémenter** l'interface `IAuthServiceClient.cs` et son implémentation `AuthServiceClient.cs` (utilisant `HttpClient`).
3. Ajouter une méthode `Task<List<UserSummaryDTO>> GetUserSummariesAsync(List<long> userIds)` qui appelle l'API de `gitdock-auth` (ex: `POST /api/auth/users/internal/summaries`).
4. Dans `Controllers/LeaderboardController.cs` (à créer), utiliser ce client pour enrichir les scores de la base de données locale avec les noms et prénoms venant de `auth`.

---

## MISSION 3 : COUCHE ASYNCHRONE - CONSOMMATEURS (RabbitMQ)
**Objectif :** Réagir aux actions des développeurs pour donner de l'XP.
1. **Créer** les DTOs dans `DTOs/` : `CommitSavedEventDTO.cs`, `TaskCompletedEventDTO.cs`, `UserDeletedEventDTO.cs`.
2. **Créer** un dossier `Messaging/Consumers/`.
3. **Implémenter** `CommitSavedConsumer.cs` : Écoute `project.commit.saved`. Action : Ajouter de l'XP au `UserId` selon la taille du commit, mettre à jour `UserTagProgress`, et vérifier le passage de niveau.
4. **Implémenter** `TaskCompletedConsumer.cs` : Écoute `task.completed`. Action : Ajouter de l'XP au `UserId` selon le niveau de la tâche (`TaskLevel`).
5. **Implémenter** `UserDeletedConsumer.cs` : Écoute `user.deleted`. Action : Supprimer ou anonymiser le `UserGamificationProfile` associé.

---

## MISSION 4 : COUCHE ASYNCHRONE - PRODUCTEURS (RabbitMQ)
**Objectif :** Alerter le système quand un joueur gagne.
1. **Créer** le DTO `NotificationEventDTO.cs`.
2. **Créer** un dossier `Messaging/Producers/`.
3. **Implémenter** `NotificationProducer.cs` pour publier sur `notification.routing.key`.
4. Modifier les services métiers (ex: `BadgeService.cs` ou `LevelService.cs`) : Lorsqu'un utilisateur gagne un badge ou monte de niveau, utiliser `NotificationProducer` pour déclencher l'envoi d'une notification temps réel.