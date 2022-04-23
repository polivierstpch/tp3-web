<template>
  <form @submit.prevent="soumettreEvent">
    <div>
      <label for="titre"> Titre </label>
      <input
        type="text"
        name="titre"
        v-model="formEvent.titre"
        :disabled="!isEditing"
      />
    </div>
    <div>
      <label for="description">Description</label>
      <input
        type="text"
        name="description"
        v-model="formEvent.description"
        :disabled="!isEditing"
      />
    </div>
    <div>
      <label for="organisateur">Organisateur</label>
      <input
        type="text"
        name="organisateur"
        v-model="formEvent.organisateur"
        :disabled="!isEditing"
      />
    </div>
    <div>
      <label for="ville">Ville</label>
      <select name="ville" v-model="villeSelectionnee" :disabled="!isEditing">
        <option v-for="ville in villes" :key="ville.id" :value="ville">
          {{ option.nom }}
        </option>
      </select>
      <label for="region">Région</label>
      <input type="text" name="region" disabled :value="regionSelectionnee" />
    </div>
    <div>
      <label for="adresseCivique">Adresse civique</label>
      <input
        type="text"
        name="adresseCivique"
        v-model="formEvent.adresseCivique"
        :disabled="!isEditing"
      />
    </div>
    <div>
      <label for="categories">Catégories</label>
      <select
        multiple
        v-model="categoriesSelectionnnees"
        :disabled="!isEditing"
      >
        <option
          v-for="categorie in categories"
          :key="categorie.id"
          :value="categorie"
        >
          {{ categorie.nom }}
        </option>
      </select>
    </div>
    <div>
      <label for="dateDebut">Date Début</label>
      <input type="date" name="dateDebut" v-model="formEvent.periode.debut" :disabled="!isEditing" />
    </div>
    <div>
      <label for="dateFin"></label>
      <input type="date" name="dateFin" v-model="formEvent.periode.fin" :disabled="!isEditing" />
    </div>
    <div>
      <label for="nbParticipations">Nombre de participations enregistrées</label>
      <input type="text" name="nbParticipations" :value="formEvent.nbParticipations" disabled />
    </div>
    <div>
      <label for="prix"></label>
      <input type="text" name="prix" v-model.number="formEvent.prix"  :disabled="!isEditing" />
    </div>
    <input v-if="isEditing" type="submit" value="Modifier" />
  </form>
</template>

<script>
import { formaterRegion } from "@/services/utils";

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
      villeSelectionnee: this.getSelectedVille(),
      categoriesSelectionnnees: this.getSelectedCategories(),
    };
  },
  methods: {
    getSelectedVille() {
      const ville = this.formEvent.ville;
      return this.villes.find((v) => v.nom === ville);
    },
    getSelectedCategories() {
      const categories = this.formEvent.categories;
      return this.categories.filter((c) => categories.includes(c.nom));
    },
    soumettreEvent() {
      this.$emit('submit', this.formEvent)
    }
  },
  computed: {
    villes() {
      return this.$store.getters.villes;
    },
    categories() {
      return this.$store.getters.categories;
    },
    regionSelectionnee() {
      return formaterRegion(this.villeSelectionnee.region);
    },
  },
  emits: ["submit"],
};
</script> 