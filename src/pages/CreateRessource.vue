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
                    <ion-select-option value="Communication">Communication</ion-select-option>
                    <ion-select-option value="Developpementpersonnel">Développement personnel</ion-select-option>
                    <ion-select-option value="IntelligenceEmotionnelle">Intelligence émotionnelle</ion-select-option>
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
                <ion-label>Ressource</ion-label>
                <ion-select ok-text="OK" cancel-text="Annuler" v-model="ressource">
                    <ion-select-option value="Activité">Activité / Jeu à réaliser</ion-select-option>
                    <ion-select-option value="Article">Article</ion-select-option>
                    <ion-select-option value="CarteDefi">Carte défi</ion-select-option>
                    <ion-select-option value="CoursAuFormatPDF">Cours au format PDF</ion-select-option>
                    <ion-select-option value="Exercice">Exercice / Atelier</ion-select-option>
                    <ion-select-option value="FicheDeLecture">Fiche de lecture</ion-select-option>
                    <ion-select-option value="Jeu">Jeu en ligne</ion-select-option>
                    <ion-select-option value="Video">Vidéo</ion-select-option>
                </ion-select>
            </ion-item>
            <ion-item>
                <ion-label>Relation</ion-label>
                <ion-select ok-text="OK" cancel-text="Annuler" v-model="relation">
                    <ion-select-option value="Soi">Soi</ion-select-option>
                    <ion-select-option value="Conjoints">Conjoints</ion-select-option>
                    <ion-select-option value="Famille">Famille : enfants / parents / fratrie</ion-select-option>
                    <ion-select-option value="Professionnelle">Professionnelle : collègues, collaborateurs et managers</ion-select-option>
                    <ion-select-option value="AmisEtCommunautes">Amis et communautés</ion-select-option>
                    <ion-select-option value="Inconnus">Inconnus</ion-select-option>
                </ion-select>
            </ion-item>
            <ion-button @click="testfunction()" shape="round" type="submit" expand="block">Créer</ion-button>
        </form>
    </base-layout>
</template>

<script>

import { IonItem, IonLabel, IonInput, IonTextarea, IonButton, IonList, IonSelectOption, IonSelect  } from '@ionic/vue';
export default {
    components: {
        IonItem, IonLabel, IonInput, IonTextarea, IonButton, IonList, IonSelectOption, IonSelect
    },
    data () {
        return{
            titre:'',
            texte:'',
            categorie:'',
            ressource:'',
            relation:''
        }
    },
    methods: {
        testfunction() {
            console.log("this.titre");
            console.log(this.titre);
            console.log("this.texte");
            console.log(this.texte);
            console.log("this.categorie");
            console.log(this.categorie);
            console.log("this.ressource");
            console.log(this.ressource);
            console.log("this.relation");
            console.log(this.relation);
        }
    }
}
</script>