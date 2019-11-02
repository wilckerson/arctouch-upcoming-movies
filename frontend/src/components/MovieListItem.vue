<template>
  <div v-if="movieItem" >
    <div
    @click="onClickMovie"
      class="movie-poster"
      :style="'background-image: url('+helpers.getImagePathW200(movieItem.posterPath)+')'"
    ></div>
    <div class="mt-3 title">{{movieItem.title}}</div>
    <div v-if="showGenre()">
      <div class="genre">{{getGenreDescription()}}</div>
      
    </div>
    <div v-if="movieItem.releaseDate">{{helpers.getReleaseDateDescription(movieItem.releaseDate)}}</div>
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
    showGenre() {
      return (
        this.movieItem.genreIds &&
        this.movieItem.genreIds.length &&
        this.genreList &&
        this.genreList.length
      );
    },
    getGenre(id) {
      return (this.genreList || []).find(item => item.id == id) || {};
    },
    getGenreDescription(){
var genreNameList = this.movieItem.genreIds.map(genreId => this.getGenre(genreId).name);
var description = genreNameList.join(", ");
return description;
    },

    onClickMovie() {
      this.$emit("click", this.movieItem);
    }
  }
};
</script>

<style scoped>

.movie-poster {
  width: 161px;
  height: 242px;
  background-position: center center;
  background-size: cover;
  box-shadow: 0px 8px 24px rgba(0,0,0,0.618033);
  cursor:pointer;
}

.genre{
  opacity: 0.618033;
  font-size:0.8rem;
}

.title{
  font-size:0.93rem;
}
</style>