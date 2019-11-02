<template>
  <div id="app">
    <movie-list></movie-list>
  </div>
</template>

<script>
//Components
import MovieList from './components/MovieList.vue'

export default {
  components: {
    MovieList
  }
}
</script>

<style>
body {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
}

</style>
