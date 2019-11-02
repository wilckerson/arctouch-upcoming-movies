<template>
  <div v-if="movieItem" style="margin-bottom: 12px;">
    <div>{{movieItem.title}}</div>
    <div v-if="showGenre()">
      <span
        v-for="genreId in movieItem.genreIds"
        :key="'genre'+genreId"
        style="background-color: #eee; display:inline-block; margin-right:4px; padding:2px 4px; border-radius:2px;"
      >{{getGenre(genreId).name}}</span>
    </div>
  </div>
</template>

<script>
export default {
  props: ["movieItem", "genreList"],
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
    }
  }
};
</script>