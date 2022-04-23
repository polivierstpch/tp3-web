<template>
  <form @submit.prevent="$emit('submit', formEvent)">
    <div>
        <label for="titre"> Titre </label>
        <input type="text" name="titre" v-model="formEvent.titre" :disabled="!isEditing"/>
    </div>
    <div>
        <label for="description">Description</label>
        <input type="text" name="description" v-model="formEvent.description" :disabled="!isEditing"/>
    </div>
    <div>
        <label for="organisateur">Organisateur</label>
        <input type="text" name="organisateur" v-model="formEvent.organisateur" :disabled="!isEditing"/>
    </div>
    <div>
        <label for="ville">Ville</label>
        <select name="ville">
            <option v-for="ville in villes" :key="ville.id" :value="ville">
                {{ option.nom }}
            </option>
        </select>
        <label for="region">Région</label>
        <input type="text" name="region" disabled :value="villeSelectionnee.region"/>
    </div>
    <div><label for=""></label><input type="text" /></div>
    <div><label for=""></label><input type="text" /></div>
    <div><label for=""></label><input type="text" /></div>
    <div><label for=""></label><input type="text" /></div>
    <div><label for=""></label><input type="text" /></div>
    <div><label for=""></label><input type="text" /></div>
    <div><label for=""></label><input type="text" /></div>
  </form>
</template>

<script>
export default {
  name: "EventForm",
  props: {
    event: {
      type: Object,
      required: true,
    },
    isEditing: {
      type: Boolean,
      required: true,
    },
  },
  data() {
    return {
      formEvent: this.props.event,
      villeSelectionnee: this.getSelectedVille()
    };
  },
  methods: {
      getSelectedVille() {
          const ville = this.formEvent.ville;
          return this.villes.find(v => v.nom == ville);
      },
      formaterRegion(region) {
          
      }
  },
  computed: {
      villes() {
          return this.$store.getters.villes;
      }
  },
  emits: ["submit"]
};
</script> 