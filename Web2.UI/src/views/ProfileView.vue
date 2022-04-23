<template>
  <main>
      <h2>Profil Utilisateur</h2>
      <button @click="$oidc.signOut()">Déconnexion</button>
  </main>
</template>

<script>
export default {
  name: 'ProfileView',
}
</script>
