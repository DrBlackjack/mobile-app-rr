<template>
    <base-layout pageTitle="Connexion" page-default-back-link="/start">
        <form class="ion-padding" @submit.prevent="submitForm">
            <ion-list>
                <ion-item>
                    <ion-label position="floating">Email</ion-label>
                    <ion-input type="email" required  v-model="email"/>
                </ion-item>
                <ion-item>
                    <ion-label position="floating">Mot de passe</ion-label>
                    <ion-input type="password" required v-model="mdp"/>
                </ion-item>
            </ion-list>
            <ion-button @click="testfunction()" shape="round" type="submit" expand="block">Connexion</ion-button>
            <ion-item>
                <ion-checkbox></ion-checkbox>
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
import { IonCheckbox, IonLabel, IonItem, IonNavLink, IonButton, IonInput, IonList } from "@ionic/vue";
import axios from 'axios';

export default {
    components: {
        IonCheckbox,
        IonLabel,
        IonItem,
        IonNavLink,
        IonButton,
        IonInput,
        IonList
    },
    data () {
        return{
            email:'',            
            mdp:''
        }
    },
    methods: {
        testfunction() {
            axios.get( this.$constapi + 'utilisateur/Login/' + this.email + '/' + this.mdp)
                .then(response => {
                    // JSON responses are automatically parsed.
                    console.log("succes, jeton : ");
                    console.log(response.data);
                    this.$store.dispatch("changeToken", response.data);
                    
                    console.log("email global : " + this.$store.getters.utilisateur.mail + " email de la page : " + this.email );
                    this.$store.dispatch("changeMail", this.email);
                    console.log("changement, email global : " + this.$store.getters.utilisateur.mail + " email de la page : " + this.email );
                })
                .catch(error=>  {
                    if (error?.response?.status == 400) {
                        console.log("mauvais mdp");
                    } else {
                        console.log(error);
                    }
                })
            console.log("this.email");
            console.log(this.email);
            console.log("this.mdp");
            console.log(this.mdp);
        }
    },
    created() {  console.log("create"); }
}
</script>