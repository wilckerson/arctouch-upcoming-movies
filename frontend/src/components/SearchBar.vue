<template>
  <!-- <div style="position:relative;">
    <input type="text" class="form-control" v-model="query" :placeholder="placeholder || 'Search...'" style="width:100%;"/>
    <div
        style="position: absolute;right: 3px;top: -3px;font-size: 1.5rem;cursor: pointer;color: #aaa;"
        @click="onClearQuery"
      >&times;</div>


  </div>-->

  <div class="input-group">
    <img class="search-icon" src="/img/magnifying-glass.svg" alt="Loading icon"/>
    <input
      type="text"
      class="form-control"
      v-model="query"
      :placeholder="placeholder || 'Search...'"
    />

    <span v-if="query" @click="onClearQuery" class="clear-icon">&times;</span>
  </div>
</template>

<script>
export default {
  props: ["placeholder"],
  data() {
    return {
      query: "",
      debounceTimerId: undefined,
      debounceDelay: 700
    };
  },
  watch: {
    query: function(newValue, oldValue) {
      if (newValue != oldValue) {
        clearTimeout(this.debounceTimerId);
        this.debounceTimerId = setTimeout(() => {
          this.$emit("change", newValue);
        }, this.debounceDelay);
      }
    }
  },
  methods: {
    onClearQuery() {
      this.query = "";
    }
  }
};
</script>

<style scoped>
.input-group {
  background: white;
  border-radius: 18px;
  color: #333;
}

.input-group input {
  background: transparent;
  border: none;
  /* outline: none; */
}

.search-icon{
  width:20px;
  margin-left:12px;
  margin-right:3px;
  opacity: 0.618033;
}

.clear-icon{
  cursor:pointer;
  font-size:1.5rem;
  padding-right:12px;
  padding-left:3px;
  vertical-align: middle;
  line-height: 30px;
  opacity: 0.618033;
}
</style>