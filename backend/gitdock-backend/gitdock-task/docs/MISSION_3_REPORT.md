# MISSION 3 REPORT

## Résumé des modifications effectuées
- Implémentation des mécanismes synchrones Anti-Corruption Layer (ACL).
- Création du dossier `src/Client/` servant de façade pour l'interrogation du reste du SI.
- Déploiement de `ProjectServiceClient`, responsable des requêtes HTTP (via `HttpClientInterface`) vers l'API de `gitdock-project` (permettant la validation de sûreté de `$projectId`).
- Déploiement de `AuthServiceClient`, responsable des requêtes HTTP vers l'API de `gitdock-auth` (permettant la validation réactive de `$assignedToUserId` et `$assignedByUserId`).
- Sécurisation des appels via l'interception de `\Throwable` et écriture de journaux d'erreurs dans le cas d'un service momentanément indisponible.

## Fichiers créés/modifiés/supprimés
- **[CRÉÉ]** `src/Client/ProjectServiceClient.php`
- **[CRÉÉ]** `src/Client/AuthServiceClient.php`
- **[CRÉÉ]** `docs/MISSION_3_REPORT.md`

## Commandes terminal exécutées
- Aucune commande terminal n'a été exécutée.

## Difficultés rencontrées
- La variable d'environnement relative à la Gateway n'étant pas formellement définie, l'implémentation est dotée d'une flexibilité (`$_ENV[...] ?? 'http://gitdock-...'`) couvrant sa résolution via le service interne Docker conventionnel (Résolution DNS).
