<template>
  <div>
    <!-- Search bar -->
    <div class="searchbar-container pb-3">
      <search-bar @change="onChangeSearch" placeholder="Search for a movie..."></search-bar>
    </div>

    <!-- Loading -->
    <div v-if="loading" class="text-center mt-5 mb-5">
      <img src="/img/magnify-loading.svg" alt="Loading icon" />
      <div>Searching movies...</div>
    </div>

    <div v-if="!loading" class="mt-5">
      <!-- Empty state -->
      <div
        v-if="paginatedMovieList && paginatedMovieList.results && paginatedMovieList.results.length == 0"
        class="text-center"
      >
        <h5>No results</h5>
        <div>There are no movies that matched your query.</div>
      </div>
      <!-- Movie list -->
      <div class="row mt-3">
        <div
          v-for="movieItem in paginatedMovieList.results"
          v-bind:key="'movieItem_'+movieItem.id"
          class="col-6 col-sm-4 col-md-3 col-lg-1-5 movie-item"
        >
          <movie-list-item :movie-item="movieItem" :genre-list="genreList" @click="onSelectMovie"></movie-list-item>
        </div>
      </div>
    </div>

    <!-- Pagination -->

    <paginate
      :click-handler="onChangePage"
      :page-count="paginatedMovieList.totalPages || 0"
      :hide-prev-next="true"
      prev-text="Previous"
      next-text="Next"
      next-class="page-item"
      prev-class="page-item"
      page-class="page-item"
      container-class="pagination justify-content-center"
    ></paginate>

    <!-- <div>
      <template v-for="(pageNumber,idx) in paginatedMovieList.totalPages">
        <button
          v-if="page != pageNumber"
          v-bind:key="'page'+idx"
          style="padding:4px 8px;"
          @click="onChangePage(pageNumber)"
        >{{pageNumber}}</button>
        <span v-else v-bind:key="'page'+idx" style="padding:4px 8px;">{{pageNumber}}</span>
      </template>
    </div>-->
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
  props: ["genreList"],
  components: {
    SearchBar,
    MovieListItem
  },
  data() {
    return {
      loading: false,
      query: "",
      page: 1,
      paginatedMovieList: { totalPages: 0 }
    };
  },

  async created() {
    this.populateMovies();
  },

  methods: {
    async populateMovies() {
      try {
        this.loading = true;

        var response = await axios.get(
          `${config.API_URL}/movie?query=${this.query || ""}&page=${this.page ||
            ""}`
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
    onSelectMovie(movieItem) {
      if (this.loading) {
        return;
      }

      this.$emit("selectMovie", movieItem);
    }
  }
};
</script>

<style scoped>
.searchbar-container {
  max-width: 600px;
  margin: 0 auto;
}

.movie-item {
  margin-bottom: 64px;
}


</style>