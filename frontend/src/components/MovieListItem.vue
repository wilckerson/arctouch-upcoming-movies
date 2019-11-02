<template>
  <div v-if="movieItem" style="margin-bottom: 12px;" @click="onClickMovie">
      <div
      :style="'width:100px; height:161px; background-position: center center; background-size: cover; background-image: url('+helpers.getImagePathW200(movieItem.posterPath)+')'">

      </div>
    <div>{{movieItem.title}}</div>
    <div v-if="showGenre()">
      <span
        v-for="genreId in movieItem.genreIds"
        :key="'genre'+genreId"
        style="background-color: #eee; display:inline-block; margin-right:4px; padding:2px 4px; border-radius:2px;"
      >{{getGenre(genreId).name}}</span>
    </div>
    <div v-if="movieItem.releaseDate">{{helpers.getReleaseDateDescription(movieItem.releaseDate)}}</div>
  </div>
</template>

<script>
import helpers from "../helpers";

export default {
  props: ["movieItem", "genreList"],
  data(){
    return {
      helpers
    }
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
    
    onClickMovie(){
        this.$emit('click', this.movieItem);
    }
  }
};
</script>