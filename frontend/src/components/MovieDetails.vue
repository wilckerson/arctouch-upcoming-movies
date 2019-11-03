<template>
  <div>
    <div class="movie-backdrop-container">
      <div
        class="movie-backdrop"
        :style="'background-image: url('+helpers.getImagePathW1280(movieItem.backdropPath)+')'"
      ></div>
      <div class="movie-backdrop-shadow"></div>
    </div>

    <div class="container-fluid movie-info-container">
      <div class="row">
        <div class="col-sm-4 order-last order-sm-first">
          <div
            class="movie-poster mx-auto"
            :style="'background-image: url('+helpers.getImagePathW200(movieItem.posterPath)+')'"
          ></div>

          <a
            v-if="movieDetails.trailerUrl"
            :href="movieDetails.trailerUrl"
            class="btn btn-danger btn-block mt-4"
            target="_blank"
          >
            <img src="/img/play-icon.svg" alt="Play icon" width="20" class="mr-1" />
            <span>Play trailer</span>
          </a>
        </div>
        <div class="col-sm-8 mb-3">
          <h1 class="movie-title">{{movieItem.title}}</h1>
          <div v-if="movieItem.genreIds">
            <div class="movie-genre">{{helpers.getGenreDescription(genreList, movieItem.genreIds)}}</div>
          </div>

          <div class="row">
            <div class="col-8">
              <div v-if="movieItem.releaseDate" class="movie-release-date mt-1">
                <img src="/img/calendar-icon.svg" alt="Release date icon" width="20" class="mr-1" />
                <span>Release date: {{helpers.getReleaseDateDescription(movieItem.releaseDate)}}</span>
              </div>
              <div class="mt-2">
                <img src="/img/time-icon.svg" alt="Runtime icon" width="20" class="mr-1" />
                <span>Runtime: {{helpers.getRuntimeDescription(movieDetails.runtime) || " - "}}</span>
              </div>
            </div>
            <div class="col-4 text-center">
              <movie-score :score="movieItem.userScore" :big="true" alt="User score" />
              <small>User Score</small>
            </div>
          </div>

          <h6 class="mt-4">Overview</h6>
          <div class="movie-overview">{{movieItem.overview || 'No overview available.'}}</div>
        </div>
      </div>

      <div class="movie-info-details pb-5">
        <div v-if="loading" class="text-center p-5">
          <h5>Getting movie details...</h5>
        </div>
        <div v-if="!loading">
          <div v-if="movieDetails.castList && movieDetails.castList.length > 0" class="mt-4">
            <h5>Top Billed Cast</h5>

            <div class="row mt-3">
              <div
                v-for="(cast,castIdx) in movieDetails.castList.slice(0, 5)"
                :key="'cast_'+castIdx"
                class="col-6 col-md-4 col-lg-1-5 mb-3"
              >
                <div
                  class="cast-profile"
                  :style="'background-image: url('+helpers.getImagePathW500(cast.profilePath)+')'"
                ></div>
                <div class="mt-2">
                  <strong>{{cast.name}}</strong>
                </div>
                <small>{{cast.character}}</small>
              </div>
            </div>
          </div>
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

//Components
import MovieScore from "./MovieScore";

export default {
  props: ["movieItem", "genreList"],
  components: {
    MovieScore
  },
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
.movie-backdrop-container {
  position: relative;
}
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
  height: 62%;
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
  /* max-width: 242px;
  height: 363px; */
  max-width: 161px;
  height: 242px;
  background-position: center center;
  background-size: cover;
  position: relative;
}

.movie-info-container {
  margin-top: -74px;
}

.movie-release-date * {
  vertical-align: middle;
}

.cast-profile {
  width: 120px;
  height: 160px;
  background-position: center center;
  background-size: cover;
  box-shadow: 0px 8px 24px rgba(0, 0, 0, 0.618033);
}

.movie-overview {
  font-size: 0.93rem;
}

.movie-score {
  text-align: center;
  position: absolute;
  width: 100%;
  bottom: -10px;
}

@media (max-width: 576px) {
  .movie-title {
    font-size: 1.5rem;
  }
}
</style>