import { createApp } from 'vue'
import App from './App.vue'
import './style.css'   // ✅ AJOUTE CETTE LIGNE

import '@fortawesome/fontawesome-free/css/all.min.css'

import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'

import { router } from './router/router'

const vuetify = createVuetify({
  components,
  directives,
})

createApp(App)
  .use(router)
  .use(vuetify)
  .mount('#app')
