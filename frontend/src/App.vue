<template>
  <div id="app ">
    <div class="container pb-5">
      <header class="text-center py-4">
        <img src="/img/logo_normal_white.png" width="160" alt="archtouch logo" />
        <h2 class="main-title ml-3 mr-2 d-none d-sm-inline-block">|</h2>
        <h2 class="main-title d-sm-inline-block">Upcoming Movies</h2>
      </header>

      <movie-list @selectMovie="onSelectMovie" :genre-list="genreList"></movie-list>
    </div>

    <simple-modal :show="showModalDetails" @hide="onHideModalDetails" content-padding="0">
      <movie-details :movie-item="selectedMovieItem" :genre-list="genreList"></movie-details>
    </simple-modal>
  </div>
</template>

<script>
//External packages
import axios from "axios";
import Swal from "sweetalert2";

//General references
import config from "./config";

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
      selectedMovieItem: undefined,
      genreList: []
    };
  },
  created() {
    this.populateGenreList();
  },
  methods: {
    onHideModalDetails() {
      this.showModalDetails = false;
      this.selectedMovieItem = undefined;
    },
    onSelectMovie(movieItem) {
      this.showModalDetails = true;
      this.selectedMovieItem = movieItem;
    },
    async populateGenreList() {
      try {
        var response = await axios.get(`${config.API_URL}/movie/genres`);
        this.genreList = (response && response.data) || [];
      } catch (e) {
        console.error(e);

        Swal.fire({
          type: "error",
          title: "Something went wrong",
          text:
            "There was an error getting the genre list. Try again later or contact the support."
        });
      }
    }
  }
};
</script>

<style>
html {
  background-color: #1e2830;
  background-image: radial-gradient(
    circle at 70% top,
    #384a55 0%,
    #1e2830 80%
  );
  background-attachment: fixed;
  background-repeat: no-repeat;
  height: 100%;
}

body {
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: white;
  background: transparent;
}

.modal-content {
  background: #384a55 !important;
  border: none;
}

.main-title {
  font-weight: lighter;
  letter-spacing: 2px;
  vertical-align: middle;
}

.page-item a {
  font-weight: bold;
  color: white;
  padding: 0px 8px;
  outline: none;
}

.page-item.active {
  border: 1px solid white;
  border-radius: 6px;
}

@media (min-width: 992px) {
  .col-lg-1-5 {
    max-width: 20%;
  }
}
</style>
