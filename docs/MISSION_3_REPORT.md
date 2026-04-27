# MISSION 3 REPORT

## Resume des modifications

- Creation des DTOs d'evenements dans `backend/backend/DTOs/`:
  - `CommitSavedEventDTO`
  - `TaskCompletedEventDTO`
  - `UserDeletedEventDTO`
- Creation de la couche RabbitMQ `backend/backend/Messaging/Consumers/`.
- Implementation de `CommitSavedConsumer`:
  - ecoute `project.commit.saved`,
  - calcule l'XP selon la taille de commit,
  - met a jour le profil de gamification et la progression des tags,
  - verifie la montee de niveau.
- Implementation de `TaskCompletedConsumer`:
  - ecoute `task.completed`,
  - attribue l'XP selon `TaskLevel` (LOW/MEDIUM/HIGH/CRITICAL),
  - met a jour la progression utilisateur.
- Implementation de `UserDeletedConsumer`:
  - ecoute `user.deleted`,
  - supprime le `UserGamificationProfile` et les `UserTagProgress` associes.
- Ajout d'un service dedie `GamificationProgressUpdater` pour mutualiser la logique metier de progression.
- Enregistrement des consommateurs comme `HostedService` dans `Program.cs`.
- Ajout de la configuration RabbitMQ dans `appsettings.json`.
- Ajout de la dependance `RabbitMQ.Client` via NuGet.

## Liste des fichiers crees/supprimes

### Crees
- `backend/backend/DTOs/CommitSavedEventDTO.cs`
- `backend/backend/DTOs/TaskCompletedEventDTO.cs`
- `backend/backend/DTOs/UserDeletedEventDTO.cs`
- `backend/backend/Messaging/Consumers/GamificationProgressUpdater.cs`
- `backend/backend/Messaging/Consumers/CommitSavedConsumer.cs`
- `backend/backend/Messaging/Consumers/TaskCompletedConsumer.cs`
- `backend/backend/Messaging/Consumers/UserDeletedConsumer.cs`
- `docs/MISSION_3_REPORT.md`

### Modifies
- `backend/backend/Program.cs`
- `backend/backend/appsettings.json`
- `backend/backend/backend.csproj`

### Supprimes
- Aucun fichier supprime pendant la Mission 3.

## Commandes terminal lancees

- `dotnet add package RabbitMQ.Client`
- `dotnet build`

## Difficultes rencontrees

- Aucun blocage technique sur la mission. Les ecoutes RabbitMQ ont ete implementees en background services avec acknowledgement explicite des messages.
