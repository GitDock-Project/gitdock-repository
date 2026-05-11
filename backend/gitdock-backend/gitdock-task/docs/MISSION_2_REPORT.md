# MISSION 2 REPORT

## Résumé des modifications effectuées
- Création de l'entité gamifiée `TaskLevel` spécifiant la difficulté d'une tâche (nom, récompense d'expérience et relation `OneToMany` vers `Task`).
- Création du `TaskLevelRepository` associé pour gérer les interactions de persistance de cette nouvelle entité.
- Mise à jour de l'entité noyau (`Task`) en intégrant une relation `ManyToOne` en direction de `TaskLevel` (`$taskLevel`), la raccrochant ainsi au moteur de gamification en devenir.

## Fichiers créés/modifiés/supprimés
- **[CRÉÉ]** `src/Entity/TaskLevel.php`
- **[CRÉÉ]** `src/Repository/TaskLevelRepository.php`
- **[MODIFIÉ]** `src/Entity/Task.php`
- **[CRÉÉ]** `docs/MISSION_2_REPORT.md`

## Commandes terminal exécutées
- Aucune commande terminal exécutée spécifiquement pour la création de la classe.

## Difficultés rencontrées
- La présence résiduelle d'une ancienne entité `Level` a imposé de baptiser cette nouvelle entité strictement `TaskLevel` en prenant soin de ne pas altérer à tort l'ancienne s'il s'avérait qu'elle servait à un autre cas d'usage ou était ciblée pour une destruction ultérieure. Les instructions ont été suivies à la lettre.
