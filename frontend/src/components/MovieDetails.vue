<template>
  <div>
    <div v-if="loading">
      <h5>Loading...</h5>
    </div>

    <div v-if="!loading">
        <div
      :style="'width:300px; height:500px; background-position: center center; background-size: cover; background-image: url('+helpers.getImagePathW500(movieDetails.posterPath)+')'">

      </div>
      <div>{{movieDetails.title}}</div>
      <div>{{movieDetails.overview}}</div>
      <div>{{helpers.getReleaseDateDescription(movieDetails.releaseDate)}}</div>
      <div v-if="movieDetails.genreNames">
      <span
        v-for="(genreName,idx) in movieDetails.genreNames"
        :key="'genre'+idx"
        style="background-color: #eee; display:inline-block; margin-right:4px; padding:2px 4px; border-radius:2px;"
      >{{genreName}}</span>
    </div>
    </div>
  </div>
</template>

<script>
//External packages
import axios from "axios";
import Swal from "sweetalert2";

//General references
import config from "../config";
import helpers from "../helpers";

export default {
  props: ["movieId"],
  data() {
    return {
      helpers,

      loading: false,
      movieDetails: {}
    };
  },
  watch: {
    movieId: {
      immediate: true,
      handler(newVal, oldVal) {
        this.populateDetails();
      }
    }
  },
  methods: {
    async populateDetails() {
      try {
        this.loading = true;

        var response = await axios.get(
          `${config.API_URL}/movie/${this.movieId}`
        );

        this.movieDetails = (response && response.data) || {};

        this.loading = false;
      } catch (e) {
        console.error(e);

        this.loading = false;

        Swal.fire({
          type: "error",
          title: "Something went wrong",
          text:
            "There was an error getting movie details. Try again later or contact the support."
        });
      }
    }
  }
};
</script>