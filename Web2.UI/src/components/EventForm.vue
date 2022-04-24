<template>
  <form>
    <div>
      <label for="titre"> Titre </label>
      <input
        type="text"
        name="titre"
        :value="evenement.titre"
        disabled
      />
    </div>
    <div>
      <label for="description">Description</label>
      <input
        type="text"
        name="description"
        :value="evenement.description"
        disabled
      />
    </div>
    <div>
      <label for="organisateur">Organisateur</label>
      <input
        type="text"
        name="organisateur"
        :value="evenement.organisateur"
        disabled
      />
    </div>
    <div>
      <label for="ville">Ville</label>
      <select name="ville" v-model="villeSelectionnee" disabled>
        <option v-for="ville in villes" :key="ville.id" :value="ville">
          {{ ville.nom }}
        </option>
      </select>
    </div>
    <div>
      <label for="region">Région</label>
      <input type="text" name="region" disabled :value="afficherRegion(evenement.region)" />
    </div>
    <div>
      <label for="adresseCivique">Adresse civique</label>
      <input
        type="text"
        name="adresseCivique"
        :value="evenement.adresseCivique"
        disabled
      />
    </div>
    <div>
      <label for="categories">Catégories</label>
      <select multiple v-model="categoriesSelectionnees" disabled>
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
      <input type="date" name="dateDebut" :value="dateDebutDate" disabled />
      <input type="time" name="dateDebut" :value="dateDebutHeure" disabled />
    </div>
    <div>
      <label for="dateFin">Date fin</label>
      <input type="date" name="dateFin" :value="dateFin" disabled />
    </div>
    <div>
      <label for="nbParticipations">Nombre de participations enregistrées</label>
      <input type="text" name="nbParticipations" :value="evenement.nbParticipations" disabled />
    </div>
    <div>
      <label for="prix">Prix</label>
      <input type="text" name="prix" :value="prixEnDollars" disabled />
    </div>
  </form>
</template>

<script>
import { formaterRegion } from "@/services/utils";

export default {
  name: "EventForm",
  props: {
    evenement: {
      type: Object,
      required: true
    },
    categories: {
      type: Array,
      required: true
    },
    villes: {
      type: Array,
      required: true
    }
  },
  data() {
    const ville = this.evenement.ville;
    const categories = this.evenement.categories;
    return {
      villeSelectionnee: this.villes.find(v => v.nom === ville),
      categoriesSelectionnees: this.categories.filter(c => categories.includes(c.nom)) 
    }
  },
  methods: {
    afficherRegion(region) {
      return formaterRegion(region);
    },
    formaterDate(date) {
      return `${date.getFullYear()}-${(date.getMonth() + 1).toLocaleString(undefined, {minimumIntegerDigits: 2})}-${date.getDate().toLocaleString(undefined, {minimumIntegerDigits: 2})}`;
    },
    formaterHeure(date) {
      return `${date.getHours().toLocaleString(undefined, {minimumIntegerDigits: 2})}:${date.getMinutes().toLocaleString(undefined, {minimumIntegerDigits: 2})}`;
    }
  },
  computed: {
    prixEnDollars() {
      return `${this.evenement.prix.toFixed(2)} $`;
    },
    dateFin() {
      const date = this.evenement.dateFin;
      return this.formaterDate(date);
    },
    dateDebutDate() {
      const date = this.evenement.dateDebut;
      return this.formaterDate(date);
    },
    dateDebutHeure() {
      const date = this.evenement.dateDebut;
      return this.formaterHeure(date);
    }
  }
};
</script>

<style scoped>
label {
  display: block;
  font-weight: bold;
}

form div {
  margin: 10px 0;
}

input, select {
  font-family: inherit;
  font-size: 1em;
}

input[disabled], select[disabled] {
  border: 1px solid #fff;
  background-color: transparent;
  color: #fff;
}
</style>