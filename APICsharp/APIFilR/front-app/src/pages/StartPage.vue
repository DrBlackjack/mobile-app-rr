<template>
    <base-layout pageTitle="Accueil">
        <template v-slot:actions-end>
            <ion-button router-link="/login/">
                Connexion
            </ion-button>
        </template>
        <div class="ion-text-center">
            <img width="280" src="../assets/logo.png"/>
        </div>
        <div class="ion-padding">
            <ion-button shape="round" expand="block" router-link="/catalog/">Catalogue</ion-button>
            <ion-button shape="round" expand="block" router-link="/create/">Création & Partage</ion-button>
            <ion-button shape="round" expand="block">Statistiques </ion-button>
        </div>
    </base-layout>
</template>

<script>
import { IonButton } from "@ionic/vue";

export default {
    components: {
        IonButton
    }
};
</script>