import { createStore } from 'vuex'
import httpClient from '../services/httpClient'

export default createStore({
  state: {
    currentEvent: {},
    events: [],
    participations: [],
    villes: [],
    categories: []
  },
  getters: {
    currentEvent: state => state.currentEvent,
    events: state => state.events,
    participations: state => state.participations,
    villes: state => state.villes,
    categories: state => state.categories
  },
  mutations: {
    setCurrentEvent(state, payload) {
      state.currentEvent = payload;
    },
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
            dateDebut: new Date(evt.dateDebut),
            dateFin: new Date(evt.dateFin),
            nbParticipations: evt.nbParticipations,
            prix: evt.prix
          };
        });
        commit('setEvents', events);
      } catch (e) {
        console.log(e);
      }
    },
    async getEventParId({ commit }, id) {
      try {
        const response = await httpClient.get(`/api/evenements/${id}`)
        if(!response.data) return;
        if (response.status === 404) {
          throw new Error('Not found');
        }
        
        const event = {
          id: response.data.id,
          titre: response.data.titre,
          description: response.data.description,
          organisateur: response.data.nomOrganisateur,
          ville: response.data.ville,
          adresseCivique: response.data.adresseCivique,
          region: response.data.region,
          categories: response.data.categories,
          dateDebut: new Date(response.data.dateDebut),
          dateFin: new Date(response.data.dateFin),
          nbParticipations: response.data.nbParticipations,
          prix: response.data.prix
        };
        commit('setCurrentEvent', event);
      } catch (e) {
        console.log(e);
      }
    },
    // async getParticipationsForEvent() {

    // },
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
