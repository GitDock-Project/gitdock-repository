# ARBORESCENCE CIBLE POUR GITDOCK-TASK (SYMFONY)

```text
gitdock-task/
+- config/
¦  +- packages/
¦  ¦  +- messenger.yaml             <-- Configuration RabbitMQ (Routing)
¦  ¦  +- ...
+- src/
¦  +- Client/                       <-- NOUVEAU: Appels HTTP Synchrones (Feign equivalent)
¦  ¦  +- AuthServiceClient.php
¦  ¦  +- ProjectServiceClient.php
¦  +- Controller/
¦  ¦  +- TaskController.php
¦  ¦  +- TaskLevelController.php    <-- NOUVEAU (Optionnel, pour le CRUD des niveaux)
¦  +- DTOs/                         <-- NOUVEAU: Contrats de données inter-services
¦  ¦  +- CommitSavedEventDTO.php
¦  ¦  +- NotificationEventDTO.php
¦  ¦  +- ProjectDeletedEventDTO.php
¦  ¦  +- TaskCompletedEventDTO.php
¦  ¦  +- UserDeletedEventDTO.php
¦  +- Entity/                       <-- PURGÉ: Uniquement le domaine des Tâches
¦  ¦  +- Epic.php
¦  ¦  +- Part.php
¦  ¦  +- Task.php                   <-- Refactorisé avec projectId, assignedToUserId
¦  ¦  +- TaskLevel.php              <-- NOUVEAU
¦  +- Enum/
¦  ¦  +- TaskStatusEnum.php         <-- TO DO / IN_PROGRESS / DONE
¦  +- MessageHandler/               <-- NOUVEAU: Consommateurs RabbitMQ
¦  ¦  +- CommitSavedHandler.php     <-- Smart Close
¦  ¦  +- ProjectDeletedHandler.php  <-- Cascade Delete
¦  ¦  +- UserDeletedHandler.php     <-- Unassign
¦  +- MessageProducer/              <-- NOUVEAU: Producteurs RabbitMQ
¦  ¦  +- NotificationSender.php
¦  ¦  +- TaskEventPublisher.php     <-- Pour émettre TaskCompletedEvent
¦  +- Repository/
¦  ¦  +- EpicRepository.php
¦  ¦  +- PartRepository.php
¦  ¦  +- TaskLevelRepository.php
¦  ¦  +- TaskRepository.php
¦  +- Service/
¦     +- TaskService.php            <-- Logique métier épurée