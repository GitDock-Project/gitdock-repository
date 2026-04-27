# MISSION 1 REPORT

## Résumé des modifications effectuées
- Suppression totale des entités corrompues (`User`, `Project`, `Branch`, `Commit`) et de leurs `Repository` associés, isolant ainsi `gitdock-task` de ce qui ne lui appartient pas dans une architecture microservices pure.
- Modification de l'entité `Task` : recâblage complet en supprimant les relations Doctrine `ManyToOne` / `OneToMany` vers `User` et `Commit`, les remplaçant par des IDs entiers stricts (`projectId`, `assignedToUserId`, `assignedByUserId`).
- Ajustement de l'entité `Part`, `Epic` et `Level` pour remplacer elles-aussi les relations `ManyToOne` vers le défunt objet `Project` par des propriétés entières `projectId`.
- Refactoring du `TaskService` afin de cesser l'injection obsolète du `UserRepository` et d'assigner directement les identifiants d'utilisateur retournés par le DTO.
- Correction en cascade dans `TaskController` : suppression de l'exposition inutile des utilisateurs (`users`) via le point d'entrée `formData`, et adaptation des `getters` lors du formatage JSON (utilisation des IDs à la place des appels méthode sur l'ancien objet `User`).

## Fichiers créés/modifiés/supprimés
- **[SUPPRIMÉ]** `src/Entity/User.php`
- **[SUPPRIMÉ]** `src/Entity/Project.php`
- **[SUPPRIMÉ]** `src/Entity/Branch.php`
- **[SUPPRIMÉ]** `src/Entity/Commit.php`
- **[SUPPRIMÉ]** `src/Repository/UserRepository.php`
- **[SUPPRIMÉ]** `src/Repository/ProjectRepository.php`
- **[SUPPRIMÉ]** `src/Repository/BranchRepository.php`
- **[SUPPRIMÉ]** `src/Repository/CommitRepository.php`
- **[MODIFIÉ]** `src/Entity/Task.php`
- **[MODIFIÉ]** `src/Entity/Part.php`
- **[MODIFIÉ]** `src/Entity/Epic.php`
- **[MODIFIÉ]** `src/Entity/Level.php`
- **[MODIFIÉ]** `src/Service/TaskService.php`
- **[MODIFIÉ]** `src/Controller/TaskController.php`
- **[CRÉÉ]** `docs/MISSION_1_REPORT.md`

## Commandes terminal exécutées
- Commande `Remove-Item` pour supprimer dynamiquement tous les fichiers ciblés (Entités redondantes et Repositories).
- Tentative de `php bin/console cache:clear` (ignorée car l'environnement externe à docker ne possède pas l'exécutable PHP).

## Difficultés rencontrées
- La suppression de l'entité `Project` impactait également `Part`, `Epic` et `Level`. Il a donc fallu répercuter le changement des relations vers ces entités également (ajout d'une propriété entière `projectId`).
- `TaskController` référençait les méthodes de l'objet supprimé (`getFullName()` etc.). Cela nécessitait une mise à jour pour s'appuyer exclusivement sur la transmission des IDs.
