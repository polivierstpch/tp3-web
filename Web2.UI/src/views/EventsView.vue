<template>
  <div>
    <router-view />
    <EventsList />
  </div>
</template>

<script>
import EventsList from '@/components/EventsList.vue'

export default {
  name: 'HomeView',
  components: {
    EventsList
  }
}
</script>
