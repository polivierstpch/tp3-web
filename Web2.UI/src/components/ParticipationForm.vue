<template>
  <form @submit.prevent="$emit('submit', formParticipation)">
    <div>
        <label for="courriel">Adresse courriel</label>
        <input type="text" name="courriel" placeholder="participer@eventtracker.com" v-model="formParticipation.courriel" :disabled="!isEditing"/>
    </div>
    <div>
        <label for="prenom">Prénom</label>
        <input type="text" name="prenom" placeholder="John" v-model="formParticipation.prenom" :disabled="!isEditing"/>
    </div>
    <div>
        <label for="Nom">Nom</label>
        <input type="text" name="Nom" placeholder="Doe" v-model="formParticipation.Nom" :disabled="!isEditing"/>
    </div>
        <div>
        <label for="nbPlace">Nombre de place</label>
        <input type="number" name="nbPlace" placeholder="1" min="1" max="10" v-model.number="formParticipation.nbPlace" :disabled="!isEditing"/>
    </div>
     <input type="submit" value="Réserver" />
  </form>
</template>

<script>
export default {
  name: "ParticipationForm",
  props: {
    participation: {
      type: Object,
      required: true,
    }
  },
  data() {
    return {
      formParticipation: this.props.event
    };
  },
  methods: {

  },
  computed: {

  },
  emits: ["submit"]
};
</script> 