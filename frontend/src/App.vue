<template>
  <div id="app ">
    <div class="container pb-5">
      <header class="text-center py-4">
        <img src="/img/logo_normal_white.png" width="160" />
        <h2 class="main-title ml-3 mr-2 d-none d-sm-inline-block">|</h2>
        <h2 class="main-title d-sm-inline-block">Upcoming Movies</h2>
      </header>


      <movie-list @selectMovie="onSelectMovie"></movie-list>


    </div>
    
    <simple-modal :show="showModalDetails" @hide="onHideModalDetails">
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
    MovieList,
    SimpleModal,
    MovieDetails
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
    onSelectMovie(movieItem) {
      this.showModalDetails = true;
      this.selectedMovieId = movieItem.id;
    }
  }
};
</script>

<style>
body {
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: white;
  background: rgb(56, 74, 85);
  background: radial-gradient(
    circle at 70% top,
    rgba(56, 74, 85, 1) 0%,
    rgba(30, 40, 48, 1) 100%
  );
  background-attachment: fixed;
}

.main-title {
  font-weight: lighter;
  letter-spacing: 2px;
  vertical-align: middle;
}



.page-item a{
  font-weight: bold;
    color: white;
    padding: 0px 8px;
    outline: none;
}


.page-item.active{
  
  border: 1px solid white;
  border-radius: 6px;
}
</style>
