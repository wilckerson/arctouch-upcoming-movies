<template>
  <div v-bind:class="{ 'big': big }">
    <div v-if="score == 0" class="badge badge-secondary">{{getUserScoreDescription()}}</div>
    <div v-else-if="score >= 7" class="badge badge-success">{{getUserScoreDescription()}}</div>
    <div v-else-if="score >= 4" class="badge badge-warning text-white">{{getUserScoreDescription()}}</div>
    <div v-else class="badge badge-danger">{{getUserScoreDescription()}}</div>
  </div>
</template>

<script>
export default {
  props: ["score","big"],
  methods: {
    getUserScoreDescription() {
      if (!this.score) {
        return "NR";
      }

      var scoreValue = parseFloat(this.score) || 0.0;
      return scoreValue == 10 ? scoreValue : scoreValue.toFixed(1);
    }
  }
};
</script>

<style scoped>
.big .badge{
    padding: 8px 12px;
    font-size: 1rem;
}
</style>