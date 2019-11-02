<template>
  <div>
    <h1>Movie List</h1>

    <search-bar @change="onChangeSearch"></search-bar>

    <div v-if="loading" class="text-center">
      <h5>Loading...</h5>
    </div>

    <div v-if="!loading">
      <div v-for="movieItem in paginatedMovieList.results" v-bind:key="'movieItem_'+movieItem.id">
          <movie-list-item :movie-item="movieItem"></movie-list-item>
      </div>
    </div>
  </div>
</template>

<script>
//External packages
import axios from "axios";
import Swal from "sweetalert2";

//Components
import SearchBar from "./SearchBar";
import MovieListItem from "./MovieListItem";

//General references
import config from "../config";

export default {
  components: {
    SearchBar,
    MovieListItem
  },
  data() {
    return {
      loading: false,
      paginatedMovieList: []
    };
  },

  created() {
    this.populateMovies();
  },

  methods: {
    async populateMovies(query) {
      try {
        this.loading = true;

        var response = await axios.get(`${config.API_URL}/movie?query=${query || ""}`);

        this.paginatedMovieList = (response && response.data) || {};

        this.loading = false;
      } catch (e) {
        console.error(e);

        this.loading = false;

        Swal.fire({
          type: "error",
          title: "Something went wrong",
          text:
            "There was an error getting the movies. Try again later or contact the support."
        });
      }
    },
    onChangeSearch(query) {
      this.populateMovies(query);
    }
  }
};
</script>