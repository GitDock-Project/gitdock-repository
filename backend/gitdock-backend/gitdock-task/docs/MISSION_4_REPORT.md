# MISSION 4 REPORT

## Résumé des modifications effectuées
- Implémentation du système "Event-Driven" de base pour émettre des événements au reste du SI (Couche Asynchrone).
- Création des objets de transfert (DTOs) au sein du namespace `App\DTOs` : `TaskCompletedEventDTO` (pour transmettre les informations de gamification lors de la résolution d'une tâche) et `NotificationEventDTO` (pour envoyer différentes notifications utilisateur).
- Implémentation du service producteur `NotificationSender` utilisant `MessageBusInterface` avec la construction d'un `AmqpStamp` ciblant explicitement la clé de routage `notification.routing.key`.
- Implémentation du service producteur `TaskEventPublisher` envoyant les événements de complétion sur `task.completed`.
- Refactoring stratégique du coeur métier (`TaskService.php` : paramètre `update`) : Désormais, si le statut (`status`) de la tâche bascule algorithmiquement à `Done` et qu'elle ne l'était pas avant, un appel est fait au `TaskEventPublisher` pour orchestrer l'événement de réussite (en envoyant `id`, `projectId`, `assignee` et `$xpReward`).

## Fichiers créés/modifiés/supprimés
- **[CRÉÉ]** `src/DTOs/TaskCompletedEventDTO.php`
- **[CRÉÉ]** `src/DTOs/NotificationEventDTO.php`
- **[CRÉÉ]** `src/MessageProducer/NotificationSender.php`
- **[CRÉÉ]** `src/MessageProducer/TaskEventPublisher.php`
- **[MODIFIÉ]** `src/Service/TaskService.php`
- **[CRÉÉ]** `docs/MISSION_4_REPORT.md`

## Commandes terminal exécutées
- Aucune commande terminal directe n'a été nécessaire.

## Difficultés rencontrées
- La gestion stricte des objets via `MessageBusInterface` en mode couplé rabbitMQ nécessite l'utilisation native de `AmqpStamp`. Au vu des standards stricts, l'injection de cette propriété a été choisie plutôt qu'un contournement yaml statique pour garantir le respect de l'instruction sur la clef de routage à chaud.
