<template>
  <div class="content">
      <h1>Réservez vos places</h1>
      <h3>
        Prenez place à une expérience inoubliable avec  <img src="../assets/LogoEventTracker.png" alt="Logo Event Tracker">
      </h3>
     <ParticipationsForm @submit="ajouterParticipation"/>
  </div>
</template>

<script>
export default {
  name: 'HomeView',
  methods: {
      ajouterParticipation(participation) {
          const payload = { eventId: $route.params.id, participation };
          this.$store.dispatch("ajouterParticipation", payload);
      }
  }
}
</script>

<style>
.content {
  display: flex;
  flex-direction: column;
  text-align: justify;
}

.content ul {
  padding: 0 1em;
}

.content img {
  width: 200px;
}
</style>
