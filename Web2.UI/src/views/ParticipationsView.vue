<template>
  <div class="content">
      <h1>Réservez vos places</h1>
      <h3>
        Participez à une expérience inoubliable avec <img src="../assets/LogoEventTracker.png" alt="Logo Event Tracker">
      </h3>
     <ParticipationsForm @submit="ajouterParticipation"/>
  </div>
</template>

<script>
export default {
  name: 'HomeView',
  methods: {
      isValid(participation){
          const emailRegex = /^[a-zA-Z0-9_!#$%&’*+/=?`{|}~^.-]+@[a-zA-Z0-9.-]+$/;
          if(this.courriel == !emailRegex.test(participation.email)){
              return false;
          }
          if(this.prenom == !RegExp || this.prenom.length <3 || this.prenom.length > 30){
              return false;
          }
          if(this.nom == !RegExp || this.nom.length <3 || this.nom.length > 30){
              return false;
          }
          if(this.nbPlace == 0 || this.nbPlace == null || this.nbPlace < 3 || this.nbPlace > 10){
              return false;
          }
          return true;
      },
      ajouterParticipation(participation) {
          if(!this.isValid(this.participation)){
              return "Veuillez corriger les erreurs dans le formulaire afin de procéder";
          }
          const payload = { eventId: this.$route.params.id, participation };
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
  width: 100px;
}
</style>
