<template>
  <ion-button @click="setOpen(true, $event)">Filtres</ion-button>
  <ion-popover
    :is-open="isOpenRef"
    css-class="my-custom-class"
    :event="event"
    :translucent="true"
    @didDismiss="setOpen(false)"
  >
    <Filters></Filters>
  </ion-popover>
</template>

<script lang="ts">
import { IonButton, IonPopover } from '@ionic/vue';
import { defineComponent, ref } from 'vue';
import Filters from '../base/Filters.vue'

export default defineComponent({
  components: { IonButton, IonPopover, Filters },
  setup() {
    const isOpenRef = ref(false);
    const event = ref();
    const setOpen = (state: boolean, ev?: Event) => {
      event.value = ev; 
      isOpenRef.value = state;
    }
    return { isOpenRef, setOpen, event }
  }
});
</script>