<template>
  <div>
    <h1>Movie List</h1>

    <search-bar @change="onChangeSearch"></search-bar>

    <div v-if="loading" class="text-center">
      <h5>Loading...</h5>
    </div>

    <div v-if="!loading" style="margin-top: 12px;">
      <div v-for="movieItem in paginatedMovieList.results" v-bind:key="'movieItem_'+movieItem.id">
        <movie-list-item :movie-item="movieItem" :genre-list="genreList" @click="onSelectMovie"></movie-list-item>
      </div>
    </div>

    <!-- Pagination -->
    <div>
      <template v-for="(pageNumber,idx) in paginatedMovieList.totalPages">
        <button
          v-if="page != pageNumber"
          v-bind:key="'page'+idx"
          style="padding:4px 8px;"
          @click="onChangePage(pageNumber)"
        >{{pageNumber}}</button>
        <span v-else v-bind:key="'page'+idx" style="padding:4px 8px;">{{pageNumber}}</span>
      </template>
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
      query: "",
      page: 1,
      paginatedMovieList: {},
      genreList: []
    };
  },

  async created() {
     await this.populateGenreList();
    this.populateMovies();
  },

  methods: {
      async populateGenreList() {
      try {
        this.loading = true;

        var response = await axios.get(`${config.API_URL}/movie/genres`);

        this.genreList = (response && response.data) || [];

        this.loading = false;
      } catch (e) {
        console.error(e);

        this.loading = false;

        Swal.fire({
          type: "error",
          title: "Something went wrong",
          text:
            "There was an error getting the genre list. Try again later or contact the support."
        });
      }
    },
    async populateMovies() {
      try {
        this.loading = true;

        var response = await axios.get(
          `${config.API_URL}/movie?query=${this.query || ""}&page=${this.page || ""}`
        );

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
      this.query = query;
      this.page = 1;
      this.populateMovies();
    },
    onChangePage(pageNumber) {
      if (this.loading) {
        return;
      }
      this.page = pageNumber;
      this.populateMovies();
    },
    onSelectMovie(movieItem){
        console.log("selectMovie", movieItem);
        this.$emit("selectMovie", movieItem);
    }
  }
};
</script>