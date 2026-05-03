# ARBORESCENCE CIBLE POUR GITDOCK-GAMIFICATION (.NET CORE)

```text
gitdock-gamification/
+- backend/
   +- backend/
      +- appsettings.json
      +- Clients/                       <-- NOUVEAU: Appels HTTP Synchrones
      ¦  +- IAuthServiceClient.cs
      ¦  +- AuthServiceClient.cs
      +- Controllers/
      ¦  +- BadgesController.cs
      ¦  +- LeaderboardController.cs    <-- NOUVEAU: Consolide les données
      ¦  +- LevelsController.cs
      ¦  +- TagsController.cs
      ¦  +- XpConfigController.cs
      +- Data/
      ¦  +- ApplicationDbContext.cs     <-- Purgé des anciennes entités User
      +- Domain/
      ¦  +- Badge.cs
      ¦  +- Level.cs
      ¦  +- Tag.cs
      ¦  +- UserGamificationProfile.cs  <-- NOUVEAU: Remplace l'entité User
      ¦  +- UserTagProgress.cs          <-- NOUVEAU
      ¦  +- XpConfig.cs
      +- DTOs/
      ¦  +- CommitSavedEventDTO.cs      <-- Reçu de Project
      ¦  +- NotificationEventDTO.cs     <-- Envoyé à Notification
      ¦  +- TaskCompletedEventDTO.cs    <-- Reçu de Task
      ¦  +- UserDeletedEventDTO.cs      <-- Reçu de Auth
      ¦  +- UserSummaryDTO.cs           <-- Reçu de Auth (Leaderboard)
      +- Messaging/                     <-- NOUVEAU: Couche RabbitMQ
      ¦  +- Consumers/
      ¦  ¦  +- CommitSavedConsumer.cs
      ¦  ¦  +- TaskCompletedConsumer.cs
      ¦  ¦  +- UserDeletedConsumer.cs
      ¦  +- Producers/
      ¦     +- NotificationProducer.cs
      +- Services/
      ¦  +- BadgeService.cs
      ¦  +- LevelService.cs
      ¦  +- XpConfigService.cs
      +- Program.cs                     <-- Injection des dépendances (RabbitMQ, HttpClient)