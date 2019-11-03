<template>
  <div style="min-height:800px;">
    <div
      class="movie-backdrop"
      :style="'background-image: url('+helpers.getImagePathW500(movieItem.backdropPath)+')'"
    >
      <div class="movie-backdrop-shadow"></div>
      <div class="container-fluid">
        <div class="row movie-info-container">
          <div class="col-4">
            <div
              class="movie-poster mx-auto"
              :style="'background-image: url('+helpers.getImagePathW500(movieItem.posterPath)+')'"
            ></div>
          </div>
          <div class="col-8">
            <h1>{{movieItem.title}}</h1>
            <div v-if="movieItem.genreIds">
              <div
                class="movie-genre"
              >{{helpers.getGenreDescription(genreList, movieItem.genreIds)}}</div>
            </div>

            <div v-if="movieItem.releaseDate" class="movie-release-date mt-1">
              <img src="/img/calendar-icon.svg" alt="Release date icon" width="20" class="mr-1" />
              <span>Release date: {{helpers.getReleaseDateDescription(movieItem.releaseDate)}}</span>
            </div>

            <h6 class="mt-4">Overview</h6>
            <div>{{movieItem.overview || 'No overview available.'}}</div>
          </div>
        </div>

        <div class="movie-info-details">
          <div v-if="loading">
            <h5>Getting movie details...</h5>
          </div>
          <div v-if="!loading">{{movieDetails}}</div>
        </div>
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
  props: ["movieItem", "genreList"],
  data() {
    return {
      helpers,

      loading: false,
      movieDetails: {}
    };
  },
  watch: {
    movieItem: {
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
          `${config.API_URL}/movie/${this.movieItem.id}`
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

<style scoped>
.movie-backdrop {
  width: 100%;
  height: 306px;
  background-position: center center;
  background-size: cover;
  background-repeat: no-repeat;
  border-top-left-radius: 0.3rem;
  border-top-right-radius: 0.3rem;
  position: relative;
}

.movie-backdrop-shadow {
  width: 100%;
  height: 189px;
  position: absolute;
  left: 0;
  bottom: 0;
  background: linear-gradient(
    0deg,
    rgb(56, 74, 85) 0%,
    rgba(255, 255, 255, 0) 100%
  );
}

.movie-poster {
  width: 242px;
  height: 363px;
  background-position: center center;
  background-size: cover;
}

.movie-info-container {
  padding-top: 232px;
}

.movie-release-date * {
  vertical-align: middle;
}
</style>