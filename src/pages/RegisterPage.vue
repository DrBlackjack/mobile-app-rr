<template>
    <base-layout pageTitle="Inscription" page-default-back-link="/start">
        <form class="ion-padding" @submit.prevent="submitForm">
            <ion-list>
                <ion-item>
                    <ion-label position="floating">Nom</ion-label>
                    <ion-input type="text" required  v-model="nom"/>
                </ion-item>
                <ion-item>
                    <ion-label position="floating">Prenom</ion-label>
                    <ion-input type="text" required  v-model="prenom"/>
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
import axios from 'axios';
import { noSpecialChar } from '../store/index.ts';

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
            nom:'',
            prenom:'',
            email:'',
            mdp:''
        }
    },
    methods: {
        testfunction() {  
            axios.get( this.$constapi + 'utilisateur/CreateAccount/' + this.email + '/' + this.mdp + '/' + noSpecialChar(this.nom) + '/' + noSpecialChar(this.prenom))
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
                    console.log("email existe déjà");
                } else {
                    console.log(error);
                }
            })
        }
    },
    created() {  
        console.log(this.$store.getters.ressources); 
        console.log(this.$store.getters.utilisateur); 
    }
}
</script>