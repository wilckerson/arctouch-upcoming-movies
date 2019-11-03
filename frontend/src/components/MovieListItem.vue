<template>
  <div v-if="movieItem">
    <div
      @click="onClickMovie"
      class="movie-poster"
      :style="'background-image: url('+helpers.getImagePathW200(movieItem.posterPath)+')'"
      alt="Movie poster"
    >
    <div class="movie-score" v-if="movieItem.userScore !== undefined">
      <movie-score :score="movieItem.userScore"/>
      
    </div>

    </div>
    
    <div class="mt-3 movie-title">{{movieItem.title}}</div>
    <div v-if="movieItem.genreIds">
      <div class="movie-genre">{{helpers.getGenreDescription(genreList, movieItem.genreIds)}}</div>
    </div>
    <div v-if="movieItem.releaseDate" class="movie-release-date">
      <img src="/img/calendar-icon.svg" alt="Release date icon" width="16" class="mr-1" />
      <span>{{helpers.getReleaseDateDescription(movieItem.releaseDate)}}</span>
    </div>
  </div>
</template>

<script>
//General references
import helpers from "../helpers";

//Components
import MovieScore from "../components/MovieScore";

export default {
  props: ["movieItem", "genreList"],
  components:{
MovieScore
  },
  data() {
    return {
      helpers
    };
  },
  methods: {
    onClickMovie() {
      this.$emit("click", this.movieItem);
    }
  }
};
</script>

<style scoped>
.movie-poster {
  max-width: 161px;
  height: 242px;
  background-position: center center;
  background-size: cover;
  box-shadow: 0px 8px 24px rgba(0, 0, 0, 0.618033);
  cursor: pointer;
  position: relative;
}

.movie-genre {
  opacity: 0.618033;
  font-size: 0.8rem;
}

.movie-title {
  font-size: 0.93rem;
}

.movie-release-date {
  opacity: 0.618033;
  font-size: 0.8rem;
}

.movie-release-date * {
  vertical-align: middle;
}

.movie-score {
  text-align: center;
    position: absolute;
    width: 100%;
    bottom: -10px;
}
</style>