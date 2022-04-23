<template>
  <div class="content">
    <img src="../assets/LogoEventTracker.png" alt="Logo Event Tracker">
    <section>
      <h3>Notre projet</h3>
      <p>
        Bienvenue sur EventTracker, 
        la destination de choix pour vivre la vie à son plein! 
        Profitez des meilleurs prix pour assister à vos événements favoris grâce à nos nombreux partenaires : 
        <strong>Whateve's Records</strong>
      </p>
      <h3>Technologies</h3>
      <p>
        <ul>
          <li>Vue.js</li>
          <li>ASP.NET Core</li>
          <li>C#</li>
          <li>Javascript</li>
        </ul>
      </p>
    </section>
    <section>
      <h3>L'équipe</h3>
      <p>
        Pier-Olivier St-Pierre-Chouinard
        <img src="https://pm1.narvii.com/7067/d71b2381b0d33f4779658ef088d0b3be4733ea83r1-1603-2048v2_hq.jpg"  width="100" alt="">
      </p>
      <p>
        Caroline Marissal-Rousseau
        <img src="https://www.pikpng.com/pngl/b/139-1394121_food-for-thought-inside-out-anger-clipart.png" width="100" alt="">
      </p>
    </section>
  </div>
</template>

<script>
export default {
  name: 'HomeView'
}
</script>

<style>
.content {
  display: flex;
  flex-direction: column;
  text-align: justify;
}

.content ul {
  padding: 0 1em;
}

.content img {
  width: 200px;
}
</style>
