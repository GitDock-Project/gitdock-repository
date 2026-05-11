
```
gitdock-task
├─ backend
│  ├─ .editorconfig
│  ├─ .env
│  ├─ .env.dev
│  ├─ .env.test
│  ├─ assets
│  │  ├─ app.js
│  │  ├─ bootstrap.js
│  │  ├─ controllers
│  │  │  ├─ csrf_protection_controller.js
│  │  │  └─ hello_controller.js
│  │  ├─ controllers.json
│  │  └─ styles
│  │     └─ app.css
│  ├─ bin
│  │  ├─ console
│  │  └─ phpunit
│  ├─ compose.yaml
│  ├─ composer.json
│  ├─ composer.lock
│  ├─ config
│  │  ├─ bundles.php
│  │  ├─ packages
│  │  │  ├─ asset_mapper.yaml
│  │  │  ├─ cache.yaml
│  │  │  ├─ csrf.yaml
│  │  │  ├─ debug.yaml
│  │  │  ├─ doctrine.yaml
│  │  │  ├─ doctrine_migrations.yaml
│  │  │  ├─ framework.yaml
│  │  │  ├─ mailer.yaml
│  │  │  ├─ messenger.yaml
│  │  │  ├─ monolog.yaml
│  │  │  ├─ nelmio_cors.yaml
│  │  │  ├─ notifier.yaml
│  │  │  ├─ property_info.yaml
│  │  │  ├─ routing.yaml
│  │  │  ├─ security.yaml
│  │  │  ├─ translation.yaml
│  │  │  ├─ twig.yaml
│  │  │  ├─ ux_turbo.yaml
│  │  │  ├─ validator.yaml
│  │  │  └─ web_profiler.yaml
│  │  ├─ preload.php
│  │  ├─ routes
│  │  │  ├─ framework.yaml
│  │  │  ├─ security.yaml
│  │  │  └─ web_profiler.yaml
│  │  ├─ routes.yaml
│  │  └─ services.yaml
│  ├─ importmap.php
│  ├─ infra
│  │  ├─ nginx
│  │  │  ├─ conf.d
│  │  │  │  └─ nginx.conf
│  │  │  └─ Dockerfile
│  │  └─ php
│  │     ├─ config
│  │     │  └─ php.ini
│  │     ├─ Dockerfile
│  │     ├─ php-fpm.d
│  │     │  └─ www.conf
│  │     └─ xdebug
│  │        └─ xdebug.ini
│  ├─ Makefile
│  ├─ migrations
│  ├─ phpunit.dist.xml
│  ├─ public
│  │  └─ index.php
│  ├─ src
│  │  ├─ Controller
│  │  │  └─ TaskController.php
│  │  ├─ DTOs
│  │  │  └─ DTOtasks
│  │  │     └─ TaskDto.php
│  │  ├─ Entity
│  │  │  ├─ Branch.php
│  │  │  ├─ Commit.php
│  │  │  ├─ Epic.php
│  │  │  ├─ Level.php
│  │  │  ├─ Part.php
│  │  │  ├─ Project.php
│  │  │  ├─ Task.php
│  │  │  └─ User.php
│  │  ├─ Enum
│  │  │  └─ ProjectEnum.php
│  │  ├─ Kernel.php
│  │  ├─ Repository
│  │  │  ├─ BranchRepository.php
│  │  │  ├─ CommitRepository.php
│  │  │  ├─ EpicRepository.php
│  │  │  ├─ LevelRepository.php
│  │  │  ├─ PartRepository.php
│  │  │  ├─ ProjectEnumRepository.php
│  │  │  ├─ ProjectRepository.php
│  │  │  ├─ TaskRepository.php
│  │  │  └─ UserRepository.php
│  │  └─ Service
│  │     └─ TaskService.php
│  ├─ symfony.lock
│  ├─ templates
│  │  ├─ base.html.twig
│  │  └─ task
│  │     └─ index.html.twig
│  ├─ tests
│  │  └─ bootstrap.php
│  └─ translations
└─ frontend
   ├─ index.html
   ├─ package-lock.json
   ├─ package.json
   ├─ postcss.config.js
   ├─ public
   │  └─ vite.svg
   ├─ README.md
   ├─ src
   │  ├─ App.vue
   │  ├─ assets
   │  │  └─ vue.svg
   │  ├─ components
   │  │  └─ HelloWorld.vue
   │  ├─ main.ts
   │  ├─ router
   │  │  └─ router.ts
   │  ├─ services
   │  │  └─ api.ts
   │  ├─ shims-styles.d.ts
   │  ├─ shims-vue.d.ts
   │  ├─ shims-vuetify.d.ts
   │  ├─ style.css
   │  ├─ views
   │  │  ├─ AddTask.vue
   │  │  ├─ DashboardTask.vue
   │  │  ├─ DeleteTask.vue
   │  │  ├─ UpdateTask.vue
   │  │  └─ ViewTask
   │  │     └─ TaskAll.vue
   │  └─ vuetify.d.ts
   ├─ tailwind.config.js
   ├─ tsconfig.app.json
   ├─ tsconfig.json
   ├─ tsconfig.node.json
   └─ vite.config.ts

```