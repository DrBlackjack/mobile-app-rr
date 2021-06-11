<template>
    <base-layout pageTitle="Création" page-default-back-link="/start">
        <form class="ion-padding" @submit.prevent="submitForm">
            <ion-list>
                <ion-item>
                    <ion-label position="floating">Titre</ion-label>
                    <ion-input type="text" required v-model="titre"/>
                </ion-item>
                <ion-item>
                    <ion-label position="floating">Écrivez votre texte ici</ion-label>
                    <ion-textarea rows="5" required v-model="texte"></ion-textarea>
                </ion-item>
            </ion-list>
            <ion-button id="outline-button" fill="outline" shape="round" expand="block">Ajouter une image</ion-button>
            <ion-item>
                <ion-label>Catégorie</ion-label>
                <ion-select ok-text="OK" cancel-text="Annuler" v-model="categorie">
                    <ion-select-option value="1">Communication</ion-select-option>
                    <ion-select-option value="2">Développement personnel</ion-select-option>
                    <ion-select-option value="3">Intelligence émotionnelle</ion-select-option>
                    <ion-select-option value="Loisirs">Loisirs</ion-select-option>
                    <ion-select-option value="MondeProfessionnel">Monde professionnel</ion-select-option>
                    <ion-select-option value="Parentalite">Parentalité</ion-select-option>
                    <ion-select-option value="QualiteDeVie">Qualité de vie</ion-select-option>
                    <ion-select-option value="RechercheDeSens">Recherche de sens</ion-select-option>
                    <ion-select-option value="SantePhysique">Santé physique</ion-select-option>
                    <ion-select-option value="SantePsychique">Santé psychique</ion-select-option>
                    <ion-select-option value="Spiritualite">Spiritualité</ion-select-option>
                    <ion-select-option value="VieAffective">Vie affective</ion-select-option>
                </ion-select>
            </ion-item>
            <ion-item>
                <ion-label>Type</ion-label>
                <ion-select ok-text="OK" cancel-text="Annuler" v-model="type">
                    <ion-select-option value="1">Activité / Jeu à réaliser</ion-select-option>
                    <ion-select-option value="2">Article</ion-select-option>
                    <ion-select-option value="3">Carte défi</ion-select-option>
                    <ion-select-option value="CoursAuFormatPDF">Cours au format PDF</ion-select-option>
                    <ion-select-option value="Exercice">Exercice / Atelier</ion-select-option>
                    <ion-select-option value="FicheDeLecture">Fiche de lecture</ion-select-option>
                    <ion-select-option value="Jeu">Jeu en ligne</ion-select-option>
                    <ion-select-option value="Video">Vidéo</ion-select-option>
                </ion-select>
            </ion-item>
            <ion-item>
                <ion-label>Relation</ion-label>
                <ion-select ok-text="OK" cancel-text="Annuler" v-model="relation" multiple="true">
                    <ion-select-option value="1">Soi</ion-select-option>
                    <ion-select-option value="2">Conjoints</ion-select-option>
                    <ion-select-option value="3">Famille : enfants / parents / fratrie</ion-select-option>
                    <ion-select-option value="Professionnelle">Professionnelle : collègues, collaborateurs et managers</ion-select-option>
                    <ion-select-option value="AmisEtCommunautes">Amis et communautés</ion-select-option>
                    <ion-select-option value="Inconnus">Inconnus</ion-select-option>
                </ion-select>
            </ion-item>
            <ion-item>
                <ion-label>Statut</ion-label>
                <ion-select ok-text="OK" cancel-text="Annuler" v-model="statut">
                    <ion-select-option value="1">Oui</ion-select-option>
                    <ion-select-option value="2">Non</ion-select-option>
                </ion-select>
            </ion-item>
            <ion-button @click="testfunction()" shape="round" type="submit" expand="block">Créer</ion-button>
        </form>
    </base-layout>
</template>

<script type="ts">
/* eslint-disable */
import { IonItem, IonLabel, IonInput, IonTextarea, IonButton, IonList, IonSelectOption, IonSelect  } from '@ionic/vue';
import axios from 'axios';

export default {
    components: {
        IonItem, IonLabel, IonInput, IonTextarea, IonButton, IonList, IonSelectOption, IonSelect
    },
    data () {
        return{
            titre:'',
            texte:'',
            categorieArray: [{}],
            categorie:'',            
            typeArray: [{}],
            type:'',
            relationArray: [{}],
            relation:[],
            statut:''
        }
    },
    methods: {
        testfunction() {
            const config = {
                headers: { Authorization: `Bearer ${this.$store.getters.utilisateur.token}` }
            };
            const ressource = {
                titre_ressource : this.titre, 
                description_ressource : this.texte,
                id_type : this.type,
                id_categories : this.categorie,
                id_utilisateur : this.statut
            };
            console.log({
                    ressource : ressource,
                    relations : this.relation
                });
            console.log(config);
            axios.post(this.$constapi + 'Ressources/PostRessource'  + '/' + this.$store.getters.utilisateur.mail,
                {
                    ressource : ressource,
                    relations : this.relation
                },
                config
            )
            .then(response => {
                // JSON responses are automatically parsed.
                console.log(response.data);
                this.posts = response.data;
            })
            .catch(e => {
                console.log(e);
                this.errors.push(e);
            })
        }
    },
    create () {
        console.log(this.$store.getters.utilisateur.mail);
    }
}

</script>