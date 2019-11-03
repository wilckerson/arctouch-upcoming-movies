<template>
  <div class="input-group">
    <img class="search-icon" src="/img/magnifying-glass.svg" alt="Loading icon" />
    <input
      type="text"
      class="form-control"
      v-model="query"
      :placeholder="placeholder || 'Search...'"
      ref="input"
    />
    <img
      v-if="query"
      @click="onClearQuery"
      class="clear-icon"
      src="/img/close-icon.svg"
      alt="Clear search icon"
    />
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

          //Clear focus (Mobile close keyboard)
          //window.document.body.focus();
          this.$refs.input.blur();

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
  outline: none;
}

.search-icon {
  width: 20px;
  margin-left: 12px;
  margin-right: 3px;
  opacity: 0.618033;
}

.clear-icon {
  cursor: pointer;
  width: 12px;
  margin-right: 12px;
  opacity: 0.618033;
  filter: invert(100%);
}
</style>