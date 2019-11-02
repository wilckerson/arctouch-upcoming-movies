<template>
  <div id="app">
    <movie-list @selectMovie="onSelectMovie"></movie-list>

    <simple-modal :show="showModalDetails" @hide="onHideModalDetails">
      <div>
        Conteúdo
      </div>
    </simple-modal>
  </div>
</template>

<script>
//Components
import MovieList from "./components/MovieList.vue";
import SimpleModal from "./components/SimpleModal.vue";

export default {
  components: {
    MovieList, SimpleModal
  },
  data() {
    return {
      showModalDetails: false
    };
  },
  methods: {
    onHideModalDetails() {
      this.showModalDetails = false;
    },
    onSelectMovie(){
      this.showModalDetails = true;
    }
  }
};
</script>

<style>
body {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
}
</style>
