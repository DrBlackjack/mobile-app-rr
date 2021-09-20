<template>
  <ion-button @click="openPopover">Filtres</ion-button>
</template>

<script  lang="ts">
import { IonButton, popoverController } from '@ionic/vue';
import Popover from './Filters.vue'

export default {
  components: { IonButton },
  methods: {
    async openPopover(ev: Event) {
      const popover = await popoverController
        .create({
          component: Popover,
          cssClass: 'my-custom-class',
          event: ev,
          translucent: true
        })
      await popover.present();

      const { role } = await popover.onDidDismiss();
      console.log('onDidDismiss resolved with role', role);
    },
  },
}
</script>