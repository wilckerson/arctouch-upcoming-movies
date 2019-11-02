<template>
  <div id="app">
    <movie-list @selectMovie="onSelectMovie"></movie-list>

    <simple-modal :show="showModalDetails" @hide="onHideModalDetails"  >
      <movie-details :movie-id="selectedMovieId"></movie-details>      
    </simple-modal>
  </div>
</template>

<script>
//Components
import MovieList from "./components/MovieList.vue";
import SimpleModal from "./components/SimpleModal.vue";
import MovieDetails from "./components/MovieDetails.vue";

export default {
  components: {
    MovieList, SimpleModal, MovieDetails
  },
  data() {
    return {
      showModalDetails: false,
      selectedMovieId: undefined
    };
  },
  methods: {
    onHideModalDetails() {
      this.showModalDetails = false;
      this.selectedMovieId = undefined;
    },
    onSelectMovie(movieItem){
      this.showModalDetails = true;
      this.selectedMovieId = movieItem.id;
    }
  }
};
</script>

<style>
body {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
}
</style>
