<template>
  <div v-if="movieItem" >
    <div
    @click="onClickMovie"
      class="movie-poster"
      :style="'background-image: url('+helpers.getImagePathW200(movieItem.posterPath)+')'"
    alt="Movie poster"></div>
    <div class="mt-3 movie-title">{{movieItem.title}}</div>
    <div v-if="movieItem.genreIds">
      <div class="movie-genre">{{helpers.getGenreDescription(genreList, movieItem.genreIds)}}</div>      
    </div>
    <div v-if="movieItem.releaseDate" class="movie-release-date">
      <img src="/img/calendar-icon.svg" alt="Release date icon" width="16" class="mr-1"/>
      <span>{{helpers.getReleaseDateDescription(movieItem.releaseDate)}}</span>
      </div>
  </div>
</template>

<script>
import helpers from "../helpers";

export default {
  props: ["movieItem", "genreList"],
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
  box-shadow: 0px 8px 24px rgba(0,0,0,0.618033);
  cursor:pointer;
}

.movie-genre{
  opacity: 0.618033;
  font-size:0.8rem;
}

.movie-title{
  font-size:0.93rem;
}

.movie-release-date{
  opacity: 0.618033;
  font-size:0.8rem;
}

.movie-release-date *{
  vertical-align: middle;
}
</style>