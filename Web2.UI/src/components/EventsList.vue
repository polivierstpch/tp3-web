<template>
  <h2>Évènements</h2>
  <table>
    <thead>
      <th>Titre</th>
      <th>Ville</th>
      <th>Nb Participations</th>
      <th>Prix</th>
      <th>Catégories</th>
      <th>Période</th>
      <th>Actions</th>
    </thead>

    <tbody v-if="events.length > 0">
      <tr v-for="event in events" :key="event.id" class="table-row">
        <td>{{ event.titre }}</td>
        <td>{{ event.ville }}</td>
        <td>{{ event.nbParticipations }}</td>
        <td>{{ event.prix.toFixed(2) + " $" }}</td>
        <td>
          <div v-for="(categorie, idx) in event.categories" :key="idx">
            {{ categorie }}
          </div>
        </td>
        <td>
          du {{ formaterDate(event.dateDebut) }}
          au
          {{ formaterDate(event.dateFin) }}
        </td>
        <td>
          <button @click="$router.push(`/evenements/${event.id}`)">Détails</button>
          <!-- <button @click="$router.push(`/evenements/${event.id}`)" >Détails</button> -->
          <button @click="$router.push({ path: `/evenements/${event.id}`, query: { enEdition: true } })">Modifier</button>
          <button v-if="isAuthenticated" @click="supprimerEvenement(event.id)">Supprimer</button>
        </td>
      </tr>
    </tbody>
    <tbody v-else>
      <tr class="table-row">
        <td colspan="7">Aucun évènement</td>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
      </tr>
    </tbody>
  </table>
</template>

<script>
export default {
  name: "EventsList",
  methods: {
    formaterDate(date) {
      return date.toLocaleString("fr-CA", {
        year: "numeric",
        month: "long",
        day: "numeric",
        hc: "h24",
        hour: "2-digit",
        minute: "2-digit",
      });
    },
    supprimerEvenement(id) {
      this.$store.dispatch('deleteEvent', id);
    }
  },
  computed: {
      events() {
          return this.$store.getters.events;
      },
      isAuthenticated() {
        return this.$oidc.isAuthenticated;
      }
  },
  async mounted() {
    await this.$store.dispatch('getEvents');
    await this.$store.dispatch('getVilles');
    await this.$store.dispatch('getCategories');
  }
};
</script>

<style>
</style>