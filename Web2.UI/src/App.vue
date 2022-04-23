<template>
  <BaseLayout>
    <router-view />
  </BaseLayout>
</template>

<script>
import BaseLayout from '@/layouts/BaseLayout.vue'

export default {
  name: 'App',
  components: {
    BaseLayout
  }
}
</script>

<style>
* {
  box-sizing: border-box;
}

html {
  background: url('./assets/BackgroundTp3Web.jpg');
  background-position: center;
  background-attachment: fixed;
  background-color: #003383;
}

body {
  margin: 0;
  padding: 0;
}

#app {
  font-family: 'Fira Sans', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #fff;
}

</style>
