<template>
    <base-layout pageTitle="Inscription" page-default-back-link="/start">
        <form class="ion-padding" @submit.prevent="submitForm">
            <ion-list>
                <ion-item>
                    <ion-label position="floating">Pseudo</ion-label>
                    <ion-input type="text" required />
                </ion-item>
                <ion-item>
                    <ion-label position="floating">Email</ion-label>
                    <ion-input type="email" required />
                </ion-item>
                <ion-item>
                    <ion-label position="floating">Mot de passe</ion-label>
                    <ion-input type="password" required />
                </ion-item>
            </ion-list>
            <ion-button shape="round" type="submit" expand="block">S'inscrire</ion-button>
        </form>
        <div class="ion-text-center">
            <ion-nav-link router-link="/login/">Connexion</ion-nav-link>
        </div>
        <br>
    </base-layout>
</template>

<script>
import { IonLabel, IonItem, IonNavLink } from "@ionic/vue"

export default {
    components: {
        IonLabel,
        IonItem,
        IonNavLink
    }
}
</script>