<template>
    <base-layout pageTitle="Catalogue" page-default-back-link="/start">
        <template v-slot:actions-end>
            <ion-button>    
                Filtre
            </ion-button>
        </template>
        <ion-searchbar></ion-searchbar>
        <ion-content>
            <ressources-list :ressources="ressources"></ressources-list>
        </ion-content>
    </base-layout>
</template>

<script>
import{ IonSearchbar } from '@ionic/vue';
import RessourcesList from "../components/ressources/RessourcesList.vue";

export default {
    components: {
        IonSearchbar,
        RessourcesList
    },
    computed: {
        ressources() {
            return this.$store.getters.ressources;
        }
    },
}

//const searchbar = document.querySelector('ion-searchbar');
//const items = Array.from(document.querySelector('ion-list').children);
/*
function handleInput(event) {
    const query = event.target.value.toLowerCase();
    requestAnimationFrame(() => {
    items.forEach(item => {
        const shouldShow = item.textContent.toLowerCase().indexOf(query) > -1;
        item.style.display = shouldShow ? 'block' : 'none';
    });
    });
}

searchbar.addEventListener('ionInput', handleInput);*/

</script>