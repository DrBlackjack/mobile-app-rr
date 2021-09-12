<template>
    <base-layout pageTitle="Catalogue" page-default-back-link="/start">
        <template v-slot:actions-end>
            <filter-button></filter-button>
        </template>
        <ion-searchbar></ion-searchbar>
        <ion-content>
            <ressources-list :ressources="ressources"></ressources-list>
        </ion-content>
    </base-layout>
</template>

<script>
import{ IonSearchbar, IonContent } from '@ionic/vue';
import axios from 'axios';
import RessourcesList from "../components/ressources/RessourcesList.vue";
import FilterButton from "../components/base/FilterButton.vue";


export default {
    components: {
        IonSearchbar,
        RessourcesList,
        FilterButton,
        IonContent,
    },
    computed: {
        ressources() {
            return this.$store.getters.ressources;
        }
    },
    created: function () {
        this.MaJRessources();
    },
    methods: {
        MaJRessources(){
            this.$store.dispatch("mazRessources");
            axios.get( this.$constapi + 'ressources/GetAllRessources')
            .then(response => {
                // tout s'est bien passé
               response.data.forEach(ressource => {
                   console.log("import de : ");
                   console.log(ressource);
                   this.$store.dispatch("ajouteRessource", { id : ressource.id_ressource, 
                                                            image : ressource.chemin_document,
                                                            title : ressource.titre_ressource,
                                                            description : ressource.description_ressource});
               });
            });
        }
    }
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