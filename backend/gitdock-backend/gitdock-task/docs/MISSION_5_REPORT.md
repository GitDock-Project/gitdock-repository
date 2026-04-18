# MISSION 5 REPORT

## Résumé des modifications effectuées
- Implémentation du système `Consumer` côté asynchrone pour réagir aux événements distants transitant par RabbitMQ.
- Création des trois DTOs entrants : `UserDeletedEventDTO`, `ProjectDeletedEventDTO` et `CommitSavedEventDTO` (contrats stricts de données).
- Création de `UserDeletedHandler.php` écoutant les suppressions d'utilisateurs (`UserDeletedEventDTO`) et passant aléatoirement toutes les tâches concernées à `assignedToUserId = null` afin de conserver la visibilité de la tâche (Ghost User/Orphan Task).
- Création de `ProjectDeletedHandler.php` écoutant les suppressions de projets (`ProjectDeletedEventDTO`) et marquant à la même seconde un `Soft Delete` en cascade sur l'ensemble des tâches historiquement associées à ce projet local.
- Création de `CommitSavedHandler.php` (Smart Close) qui parse le message de `CommitSavedEventDTO` via Regex (`/Fixes #\d+/i`). Lorsqu'une liaison est établie vers une tâche du même projet, l'état de la tâche glisse nativement à `Done`. De plus, ce statut génère le relais de la propagation en déclenchant localement le `TaskEventPublisher` pour récompenser le développeur.

## Fichiers créés/modifiés/supprimés
- **[CRÉÉ]** `src/DTOs/UserDeletedEventDTO.php`
- **[CRÉÉ]** `src/DTOs/ProjectDeletedEventDTO.php`
- **[CRÉÉ]** `src/DTOs/CommitSavedEventDTO.php`
- **[CRÉÉ]** `src/MessageHandler/UserDeletedHandler.php`
- **[CRÉÉ]** `src/MessageHandler/ProjectDeletedHandler.php`
- **[CRÉÉ]** `src/MessageHandler/CommitSavedHandler.php`
- **[CRÉÉ]** `docs/MISSION_5_REPORT.md`

## Commandes terminal exécutées
- Aucune commande terminal exécutée car l'architecture Symfony Auto-Wiring des Handlers prend la relève.

## Difficultés rencontrées
- L'utilisation directe de `TaskService::update()` (dans `CommitSavedHandler.php`) aurait rendu le Smart Close plus modulaire, par mesure de sécurité sur le comportement strict natif (le DTO ne matchait pas parfaitement avec ce besoin silencieux), l'appel a été dupliqué de manière lisible tout en respectant l'envoi du message de complétion (`task.completed`) aux autres microservices.
