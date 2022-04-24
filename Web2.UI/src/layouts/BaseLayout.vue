<template>
    <header>
        <nav>
            <router-link to="/">Accueil</router-link>
            <router-link to="/evenements">Évènements</router-link>
            <router-link to="/profil">
            {{ displayUser }}
            </router-link>
        </nav>
    </header>
    <main>
        <slot></slot>
    </main>
    <footer>
        <div>Cégep Limoilou &copy; - 2022</div>
    </footer>    
</template>

<script>
export default {
  name: 'BaseLayout',
  computed: {
      displayUser() {
          if (this.$oidc.isAuthenticated) {
              return `Compte - ${this.$oidc.userProfile.preferred_username}`;
          }
          return 'Connexion';
      }
  }
}
</script>

<style scoped>
nav a, footer div { 
  text-decoration: none;
  text-shadow: 1px 1px 2px rgba(0, 0, 0, 0.2);
  font-weight: bold;
  color: #fff;
  transition: color 0.2s ease;
}

nav a.router-link-active {
  color: #ffc000;
}

nav {
    background-color: #000;
    display: flex;
    width: 100%;
    padding: 25px;
    gap: 1em;
    box-shadow: 1px 1px 5px rgba(0, 0, 0, 0.3);
}

nav a:nth-last-child(2){
    display: flex;
    flex-grow: 1;
}

main {
    padding: 1.5em;
    margin-bottom: 3em;
}

footer {
    background-color: #003389;
    display: flex;
    position: fixed;
    width: 100vw;
    height: 3rem;
    bottom: 0;
    box-shadow: -1px -1px 5px rgba(0, 0, 0, 0.3);
}

footer div {
    margin: auto;
    padding: 0 25px;
}
</style>