<template>
    <ion-list>
        <ressource-list-item 
        v-for="ressource in ressources"
        :key="ressource.id"
        :ressource="ressource"
        ></ressource-list-item>
    </ion-list>
</template>

<script>
import { IonList } from '@ionic/vue';
import RessourceListItem from './RessourceListItem.vue';
export default {
    props: ["ressources"],
    components: {
        IonList,
        RessourceListItem
    }    
}
</script>