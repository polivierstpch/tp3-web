import { createStore } from 'vuex'
import httpClient from '../services/httpClient'

export default createStore({
  state: {
    events: [],
    participations: [],
    villes: [],
    categories: []
  },
  getters: {
    events: state => state.events,
    participations: state => state.participations,
    villes: state => state.villes
  },
  mutations: {
    setEvents(state, payload) {
      state.events = payload;
    },
    removeEvent(state, id) {
      state.events = state.events.filter(e => e.id !== id);
    },
    setParticipations(state, payload) {
      state.participations = payload;
    },
    setVilles(state, payload) {
      state.villes = payload;
    },
    setCategories(state, payload) {
      state.categories = payload;
    }
  },
  actions: {
    async getEvents({ commit }) {
      try {
        const { data } = await httpClient.get('/api/evenements');
        if (!data) return;
        const events = data.map(evt => {
          return {
            id: evt.id,
            titre: evt.titre,
            description: evt.description,
            organisateur: evt.nomOrganisateur,
            ville: evt.ville,
            adresseCivique: evt.adresseCivique,
            region: evt.region,
            categories: evt.categories,
            periode: {
              debut: new Date(evt.dateDebut),
              fin: new Date(evt.dateFin)
            },
            nbParticipations: evt.nbParticipations,
            prix: evt.prix
          };
        });
        commit('setEvents', events);
      } catch (e) {
        console.log(e);
      }
    },
    // async getParticipationsForEvent() {

    // },
    async createParticipation(){
        
    },
    // async modifyEvent({ commit }, event) {

    // },
    async deleteEvent({ commit }, id) {
      try {
        const response = await httpClient.delete(`/api/evenements/${id}`)
        if (response.status === 404) {
          throw new Error('Not found');
        }
        commit('removeEvent', id);
      } catch (e) {
        console.log(e);
      }
    },
    async getVilles({ commit }) {
      try {
        const { data } = await httpClient.get('/api/villes');
        if (!data) return;
        commit('setVilles', data);
      } catch (e) {
        console.log(e);
      }
    },
    async getCategories({ commit }) {
      try {
        const { data } = await httpClient.get('/api/categories');
        if (!data) return;
        commit('setCategories', data);
      } catch (e) {
        console.log(e);
      }
    }
  },
  modules: {
  }
})
