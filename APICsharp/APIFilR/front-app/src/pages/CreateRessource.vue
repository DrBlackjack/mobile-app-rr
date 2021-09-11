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
            <input style="display: none"
                type="file" 
                @change="onFileSelected"
                ref="fileInput">
            <ion-button id="outline-button" fill="outline" shape="round" expand="block" @click="$refs.fileInput.click()" >Ajouter une image</ion-button>
            <ion-item>
                <ion-label>Catégorie</ion-label>
                <ion-select ok-text="OK" cancel-text="Annuler" v-model="categorie">
                    <ion-select-option v-for="cat in categories" :key="cat.id_categories" :value="cat.id_categories"> {{cat.lib_categories}}</ion-select-option>
                </ion-select>
            </ion-item>
            <ion-item>
                <ion-label>Type</ion-label>
                <ion-select ok-text="OK" cancel-text="Annuler" v-model="type">
                    <ion-select-option v-for="el in types" :key="el.id_type" :value="el.id_type"> {{el.lib_type}}</ion-select-option>
                </ion-select>
            </ion-item>
            <ion-item>
                <ion-label>Relation</ion-label>
                <ion-select ok-text="OK" cancel-text="Annuler" v-model="relation" multiple="true">
                    <ion-select-option v-for="el in relations" :key="el.id_relation_ressource" :value="el.id_relation_ressource"> {{el.lib_type_relation}}</ion-select-option>
                </ion-select>
            </ion-item>
            <ion-item>
                <ion-label>Statut</ion-label>
                <ion-select ok-text="OK" cancel-text="Annuler" v-model="statut">
                    <ion-select-option v-for="el in statuts" :key="el.id_statut" :value="el.id_statut"> {{el.lib_statut}}</ion-select-option>
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
            statut:'',

            categories: [],
            types: [], 
            relations: [],
            statuts: [],
            selectedFile: null
        }
    },
    created: function () {
        this.GetInfos();
    },
    methods: {
        GetInfos(){
            axios.get( this.$constapi + 'ressources/GetCategoriesRessources')
            .then(response => {
                // tout s'est bien passé
            this.categories = response.data;
            });
            axios.get( this.$constapi + 'ressources/GetTypeRessources')
            .then(response => {
                // tout s'est bien passé
            this.types = response.data;
            });
            axios.get( this.$constapi + 'ressources/GetTypeRelationRessource')
            .then(response => {
                // tout s'est bien passé
            this.relations = response.data;
            });
            axios.get( this.$constapi + 'ressources/GetStatutRessource')
            .then(response => {
                // tout s'est bien passé
            this.statuts = response.data;
            });
        },
        testfunction() {
            const config = {
                headers: { 
                    "Content-Type" : "application/json",
                    "Authorization": `Bearer ${this.$store.getters.utilisateur.token.value}` 
                }
            };
            const json = JSON.stringify({
                    relations : this.relation,
                    titreRessource : this.titre, 
                    descriptionRessource : this.texte,
                    idType : parseInt(this.type),
                    idCategories : parseInt(this.categorie),
                    idStatut : parseInt(this.statut)
            });
            var fd = new FormData();
            fd.append("ress", json);
            fd.append('image', this.selectedFile,  this.selectedFile.name )
            console.log(fd);
            console.log(config);
            axios.post(this.$constapi + 'Ressources/PostRessource'  + '/' + this.$store.getters.utilisateur.mail,
                fd,
                config
            )
            .then(response => {
                // JSON responses are automatically parsed.
                console.log(response.data);
                this.posts = response.data;
            })
            .catch(e => {
                console.log(e);
                console.log(e.response.data);
                this.errors.push(e);
            });
        },
        onFileSelected(event){
            this.selectedFile = event.target.files[0];
        }
    }
}

</script>