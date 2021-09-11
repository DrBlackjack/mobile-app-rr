<template>
    <base-layout pageTitle="Inscription" page-default-back-link="/start">
        <form class="ion-padding" v-on:submit.prevent="onSubmit">
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
                    <ion-input type="password" pattern="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$" required v-model="mdp"/>
                </ion-item>
            </ion-list>            
            <loader v-if="chargement" position="floating"> </loader> 
            <ion-label v-if="errorMessage != ''" position="floating" style="color:#C20000">{{errorMessage}}</ion-label>
            <ion-button v-if="!chargement" @click="register()" shape="round" type="submit" expand="block">S'inscrire</ion-button>
        </form>
        <div class="ion-text-center">
            <ion-nav-link router-link="/login/">Connexion</ion-nav-link>
        </div>
        <br>
    </base-layout>
</template>

<script>
import { IonLabel, IonItem, IonNavLink, IonButton, IonInput, IonList  } from "@ionic/vue";
import axios from 'axios';
import { noSpecialChar } from '../store/index.ts';
import loader from '../utilitaires/loader.vue';

export default {
    components: {
        IonLabel,
        IonItem,
        IonNavLink,
        IonButton,
        IonInput,
        IonList,
        loader
    },
    data () {
        return{
            nom:'',
            prenom:'',
            email:'',
            mdp:'',
            errorMessage:'',
            chargement: false
        }
    },
    methods: {
        register() {  
            // Mà0 de l'erreur
            this.errorMessage = '';
            // check le formulaire
            if (this.nom != noSpecialChar(this.nom) || this.prenom != noSpecialChar(this.prenom)){
                this.showError("Veuillez ne pas utiliser de caractères spéciaux dans votre nom et prénom");
                return;
            }else if (this.email == ''){
                this.showError("Veuillez renseigner votre mail");
                return;
            }else if (!this.validEmail(this.email)){
                this.showError("Le format de votre email n'est pas correct");
                return;
            }else if (!this.validPassword(this.mdp)){
                this.showError("Votre mot de passe doit comporter au moins une majuscule, une minuscule, un chiffre, un caractere spéciale et doit faire au moins 8 caractère");
                return;
            }
            
            // On lance l'appel à l'api
            this.chargement = true;
            axios.get( this.$constapi + 'utilisateur/CreateAccount/' + this.email + '/' + this.mdp + '/' + noSpecialChar(this.nom) + '/' + noSpecialChar(this.prenom))
            .then(response => {
                // tout s'est bien passé
                this.$store.dispatch("changeToken", response.data);                
                this.$store.dispatch("changeMail", this.email);
                this.chargement = false;
                this.$router.push('../start');
            })
            .catch(error=>  {
                // ca s'est pas bien passé
                this.chargement = false;
                // code 400 codé dans l'api
                if (error?.response?.status == 400) {
                    this.showError("Cet e-email est déjà attribué");
                } else {
                    this.showError("Une erreur s'est produite, veuillez réessayer ultérieurement");
                }
            });
        },
        validEmail(email) {
            // regex pour email faut pas chercher plus loin
            const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            return re.test(email);
        },
        validPassword(mdp) {
            // 8 caractères une maj une min un chiffre un caractère spécial
            const re = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;
            return re.test(mdp);
        },
        showError(error){
            // On affice l'erreur à l'utilisateur 7 secondes
            this.errorMessage = error;
            setTimeout(function(){ this.errorMessage = ''; }.bind(this), 7000);
        }
    },
    created() {  
        console.log(this.$store.getters.ressources); 
        console.log(this.$store.getters.utilisateur); 
    }
}
</script>