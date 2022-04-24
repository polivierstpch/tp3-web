<template>
  <form @submit.prevent="$emit('submit', formParticipation)">
    <div>
        <label for="courriel">Adresse courriel</label>
        <input type="email" name="courriel" placeholder="participer@eventtracker.com" v-model="formParticipation.courriel"/>
    </div>
    <div>
        <label for="prenom">Prénom</label>
        <input type="text" name="prenom" minlenght="3" maxlenght="30" pattern="(?i)^(?:(?![×Þß÷þø])[-'0-9a-zÀ-ÿ])+$" placeholder="John" v-model="formParticipation.prenom"/>
    </div>
    <div>
        <label for="Nom">Nom</label>
        <input type="text" name="Nom" minlenght="3" maxlenght="30" pattern="(?i)^(?:(?![×Þß÷þø])[-'0-9a-zÀ-ÿ])+$" placeholder="Doe" v-model="formParticipation.Nom"/>
    </div>
        <div>
        <label for="nbPlace">Nombre de place</label>
        <input type="number" name="nbPlace" placeholder="1" min="1" max="10" v-model.number="formParticipation.nbPlace"/>
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
  create(){
      this.participationForm = {...this.event};
  },
  methods: {

  },
  computed: {

  },
  emits: ["submit"]
};
</script> 