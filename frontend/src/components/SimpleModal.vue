<template>
  <div v-if="show">
    <div class="modal-backdrop show"></div>
    <div class="modal d-block" tabindex="-1" aria-hidden="true">
      <div class="modal-dialog modal-lg">
        <div class="modal-content">
          <!-- Modal content Slot -->
          <slot></slot>

          <!-- Modal hide button -->
          <div v-if="!staticModal" class="hide-button" @click="onHideModal">
            <div class="hide-button-icon">&times;</div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
/*
<!-- Modal background -->
  <div
    v-if="show"
    style="width:100%; height:100%; background-color: rgba(0,0,0,0.5); position: fixed; top: 0; left:0;"
  >
    <!-- Modal container -->
    <div
      class="simple-modal-container"
      style="width:100%; max-width: 900px; min-height: 100px; background-color:white;margin: 60px auto 0px; position: relative;"
    >
      <!-- Modal close button -->
      <div
        style="position: absolute; right: 12px; top: 6px; font-size: 1.5rem; cursor:pointer;"
        @click="onHideModal"
      >&times;</div>

      <!-- Modal content Slot -->
      <div :style="'padding: '+(contentPadding || 12)+'px;'">
        <slot></slot>
      </div>
    </div>
  </div>
  */

export default {
  props: ["show", "params", "contentPadding", "staticModal"],
  watch: {
    show: function(newVal, oldVal) {
      if (newVal != oldVal) {
        this.bootstrapModalFix(newVal);
      }
    }
  },
  methods: {
    onHideModal() {
      this.$emit("hide");
    },
    bootstrapModalFix(add) {
      if (add) {
        window.document.body.classList.add("modal-open");
      } else {
        window.document.body.classList.remove("modal-open");
      }
    }
  }
};
</script>

<style scoped>
.hide-button {
  position: absolute;
  right: -12px;
  top: -12px;
  font-size: 1.5rem;
  cursor: pointer;
  background-color: rgb(56, 74, 85);
  width: 32px;
  height: 32px;
  text-align: center;
  border-radius: 16px;
  box-shadow: 0px 0px 3px #000;
}

.hide-button-icon {
  display: inline-block;
  line-height: 26px;
  vertical-align: top;
}

.modal {
  overflow-x: hidden;
  overflow-y: auto;
}

@media (max-width: 576px) {
  .hide-button {
    right: -6px;
    top: -6px;
  }
}
</style>