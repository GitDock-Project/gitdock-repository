# MISSION DE REFACTORING : GITDOCK-TASK (SYMFONY)

**CONTEXTE :** Ce service passe d'un monolithe isolé à un microservice Event-Driven. Il ne doit plus gérer d'utilisateurs ou de projets réels. Il est le maître absolu des Tâches, et de rien d'autre. Réfère-toi toujours au fichier `.cursorrules` avant de coder.

---

## MISSION 0 : PRÉPARATION DE L'ENVIRONNEMENT
**Objectif :** Initialiser la structure de documentation.
1. Créer le dossier `docs/` à la racine de `gitdock-task`.
2. Créer un fichier `docs/INITIAL_STATE.md` décrivant l'état actuel (monolithique) avant le début des travaux.

---

## MISSION 1 : PURGE & ISOLATION (Le grand nettoyage)
**Objectif :** Détruire les entités corrompues et recâbler l'entité Task.
1. **Supprimer** les fichiers suivants dans `src/Entity/` et leurs repositories associés dans `src/Repository/` :
   - `User.php`, `Project.php`, `Branch.php`, `Commit.php`.
2. **Modifier** `src/Entity/Task.php` :
   - Supprimer toutes les relations Doctrine (ManyToOne, etc.) pointant vers les entités supprimées.
   - Ajouter de simples propriétés entières (int) : `$projectId`, `$assignedToUserId`, `$assignedByUserId`.
3. Corriger les erreurs de compilation immédiates dans `TaskService.php` liées à ces suppressions (utiliser les IDs au lieu des objets).

---

## MISSION 2 : AJOUT DU MODÈLE MANQUANT (TaskLevel)
**Objectif :** Introduire la notion de difficulté pour la gamification.
1. **Créer** l'entité `src/Entity/TaskLevel.php` avec les propriétés : 
   - `id` (int, PK)
   - `name` (string, ex: EASY, MEDIUM, HARD)
   - `xpReward` (int).
2. **Créer** le repository associé `TaskLevelRepository.php`.
3. **Modifier** `src/Entity/Task.php` pour y ajouter une relation ManyToOne vers `TaskLevel`.

---

## MISSION 3 : COUCHE HTTP (Clients Synchrones)
**Objectif :** Permettre à Task de vérifier si les IDs reçus existent via la Gateway.
1. **Créer** un dossier `src/Client/`.
2. **Implémenter** `ProjectServiceClient.php` utilisant `Symfony\Contracts\HttpClient\HttpClientInterface` pour appeler l'API de `gitdock-project` (ex: `GET /api/projects/{id}`) afin de valider l'existence d'un projet.
3. **Implémenter** `AuthServiceClient.php` pour valider les `userId`.

---

## MISSION 4 : COUCHE ASYNCHRONE - PRODUCTEURS (RabbitMQ)
**Objectif :** Avertir le reste du système quand une tâche évolue.
1. **Créer** les DTOs dans `src/DTOs/` : `TaskCompletedEventDTO.php` et `NotificationEventDTO.php`.
2. **Créer** un dossier `src/MessageProducer/`.
3. **Implémenter** `NotificationSender.php` pour envoyer un message sur la routing key `notification.routing.key`.
4. **Implémenter** `TaskEventPublisher.php` pour publier le `TaskCompletedEventDTO` sur la routing key `task.completed`.
5. **Modifier** `TaskService.php` : Lors du passage d'une tâche au statut "DONE", utiliser Symfony Messenger via le `TaskEventPublisher` pour notifier la complétion.

---

## MISSION 5 : COUCHE ASYNCHRONE - CONSOMMATEURS (RabbitMQ)
**Objectif :** Réagir aux événements du reste du système.
1. **Créer** un dossier `src/MessageHandler/`.
2. **Implémenter** `UserDeletedHandler.php` : Écoute `UserDeletedEvent`. Action : Mettre `$assignedToUserId` à null (ou ID du Ghost User) pour les tâches de cet utilisateur.
3. **Implémenter** `ProjectDeletedHandler.php` : Écoute `ProjectDeletedEvent`. Action : Supprimer (ou soft delete) les tâches associées au `$projectId`.
4. **Implémenter** `CommitSavedHandler.php` (Smart Close) : Écoute `CommitSavedEventDTO`. Action : Parser le message du commit pour trouver "Fixes #ID", et fermer la tâche correspondante.