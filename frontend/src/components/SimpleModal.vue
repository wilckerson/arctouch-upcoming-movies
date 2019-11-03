<template>
  <transition name="fade">
    <div v-if="show">
      <div class="modal-backdrop fade show"></div>

      <div class="modal d-block" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-lg">
          <div class="modal-content">
            <!-- Modal content Slot -->
            <slot></slot>

            <!-- Modal hide button -->
            <img
              v-if="!staticModal"
              class="hide-button"
              @click="onHideModal"
              src="/img/close-icon.svg"
              alt="Clear search icon"
            />
          </div>
        </div>
      </div>
    </div>
  </transition>
</template>

<script>
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
  cursor: pointer;
  position: absolute;
  right: -12px;
  top: -12px;
  width: 32px;
  height: 32px;
  background-color: rgb(56, 74, 85);
  padding: 8px;
  border-radius: 16px;
  box-shadow: 0px 0px 3px #000;
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

.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.3s;
}
.fade-enter, .fade-leave-to /* .fade-leave-active em versões anteriores a 2.1.8 */ {
  opacity: 0;
}
</style>