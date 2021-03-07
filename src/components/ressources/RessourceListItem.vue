<template>
    <ion-item
        :router-link="`/ressource/${ressource.id}`"
    >
            <ion-thumbnail slot="start">
                <ion-img :src="ressource.image" :alt="ressource.title"></ion-img>
            </ion-thumbnail>
            <ion-label>
                {{ ressource.title }}
            </ion-label>
    </ion-item>
</template>

<script>
import { IonItem, IonThumbnail, IonImg, IonLabel } from '@ionic/vue';

export default {
    props: ["ressource"],
    components: {
        IonItem,
        IonThumbnail,
        IonImg,
        IonLabel
    }
}
</script>