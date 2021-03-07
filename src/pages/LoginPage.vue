<template>
    <base-layout pageTitle="Connexion" page-default-back-link="/start">
        <form class="ion-padding" @submit.prevent="submitForm">
            <ion-list>
                <ion-item>
                    <ion-label position="floating">Email</ion-label>
                    <ion-input type="email" required  />
                </ion-item>
                <ion-item>
                    <ion-label position="floating">Mot de passe</ion-label>
                    <ion-input type="password" required />
                </ion-item>
            </ion-list>
            <ion-button shape="round" type="submit" expand="block">Connexion</ion-button>
            <ion-item>
                <ion-checkbox slot="start"></ion-checkbox>
                <ion-label>Rester connecté</ion-label>
            </ion-item>
        </form>
        <div class="ion-text-center">
            <ion-nav-link router-link="/register/">Inscription</ion-nav-link>
        </div>
        <br>
        <div class="ion-text-center">
            <ion-nav-link router-link="/forgot/">Mot de passe oublié?</ion-nav-link>
        </div>
    </base-layout>
</template>

<script>
import { IonCheckbox, IonLabel, IonItem, IonNavLink } from "@ionic/vue";

export default {
    components: {
        IonCheckbox,
        IonLabel,
        IonItem,
        IonNavLink
    }
}
</script>