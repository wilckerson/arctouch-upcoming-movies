<template>
  <div style="position:relative;">
    <input type="text" v-model="query" :placeholder="placeholder || 'Search...'" style="width:100%;"/>
    <div
        style="position: absolute;right: 3px;top: -3px;font-size: 1.5rem;cursor: pointer;color: #aaa;"
        @click="onClearQuery"
      >&times;</div>
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
  methods:{
    onClearQuery(){
      this.query = "";
    }
  }
};
</script>