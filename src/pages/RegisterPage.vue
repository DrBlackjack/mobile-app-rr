<template>
    <base-layout pageTitle="Inscription" page-default-back-link="/start">
        <form class="ion-padding" @submit.prevent="submitForm">
            <ion-list>
                <ion-item>
                    <ion-label position="floating">Pseudo</ion-label>
                    <ion-input type="text" required v-model="uname"/>
                </ion-item>
                <ion-item>
                    <ion-label position="floating">Email</ion-label>
                    <ion-input type="email" required v-model="email"/>
                </ion-item>
                <ion-item>
                    <ion-label position="floating">Mot de passe</ion-label>
                    <ion-input type="password" required v-model="mdp"/>
                </ion-item>
            </ion-list>
            <ion-button @click="testfunction()" shape="round" type="submit" expand="block">S'inscrire</ion-button>
        </form>
        <div class="ion-text-center">
            <ion-nav-link router-link="/login/">Connexion</ion-nav-link>
        </div>
        <br>
    </base-layout>
</template>

<script>
import { IonLabel, IonItem, IonNavLink, IonButton, IonInput, IonList  } from "@ionic/vue"

export default {
    components: {
        IonLabel,
        IonItem,
        IonNavLink,
        IonButton,
        IonInput,
        IonList
    },
    data () {
        return{
            uname:'',
            email:'',
            mdp:''
        }
    },
    methods: {
        testfunction() {
            console.log("this.uname");
            console.log(this.uname);
            console.log("this.email");
            console.log(this.email);
            console.log("this.mdp");
            console.log(this.mdp);
        }
    }
}
</script>