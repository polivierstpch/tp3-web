<template>
  <div>
      <h3>Détails</h3>
      <EventForm v-if="!loading"
        :evenement="currentEvent"
        :villes="villes"
        :categories="categories"
      />
      <div v-else>Chargement...</div>
      <router-link to="/evenements">Retour</router-link>
  </div>
</template>

<script>
import EventForm from '@/components/EventForm.vue'

export default {
    name: 'EventDetailsView',
    components: {
        EventForm
    },
    data() {
        return {
            loading: false
        }
    },
    async created() {
        this.$watch(
            () => this.$route.params.id,
            async () => await this.fetchData(),
            { immediate: true }
        )
    },
    computed: {
        currentEvent() {
            return this.$store.getters.currentEvent;
        },
        villes() {
            return this.$store.getters.villes;
        },
        categories() {
            return this.$store.getters.categories;
        }
    },
    methods: {
        async fetchData() {
            if (this.$route.params.id) {
                this.loading = true;
                await this.$store.dispatch('getEventParId', this.$route.params.id);
                this.loading = false;
            }
        }
    }
}
</script>

<style>
</style>