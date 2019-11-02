<template>
  <!-- Modal background -->
  <div
    v-if="show"
    style="width:100%; height:100%; background-color: rgba(0,0,0,0.5); position: fixed; top: 0; left:0;"
  >
    <!-- Modal container -->
    <div
      style="width:100%; max-width: 600px; min-height: 100px; background-color:white;margin: 60px auto 0px; position: relative;"
    >
      <!-- Modal close button -->
      <div
        style="position: absolute; right: 12px; top: 6px; font-size: 1.5rem; cursor:pointer;"
        @click="onHideModal"
      >&times;</div>

      <!-- Modal content Slot -->
      <div style="padding: 12px;">
        <slot></slot>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: ["show", "params"],
  methods: {
    onHideModal() {
      this.$emit("hide");
    }
  }
};
</script>