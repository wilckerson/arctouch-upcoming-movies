<template>
  <div>
    <input type="text" v-model="query" :placeholder="placeholder || 'Search...'" />
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
  }
};
</script>