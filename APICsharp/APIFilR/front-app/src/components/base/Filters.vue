<template>
  <ion-content class="ion-padding">
    <ion-list>
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
            <ion-select ok-text="OK" cancel-text="Annuler" v-model="relation" multiple="true" @click="GetFilteredRessources()">
                <ion-select-option v-for="el in relations" :key="el.id_relation_ressource" :value="el.id_relation_ressource"> {{el.lib_type_relation}}</ion-select-option>
            </ion-select>
        </ion-item>
        <ion-item>
            <ion-label>Statut</ion-label>
            <ion-select ok-text="OK" cancel-text="Annuler" v-model="statut">
                <ion-select-option v-for="el in statuts" :key="el.id_statut" :value="el.id_statut"> {{el.lib_statut}}</ion-select-option>
            </ion-select>
        </ion-item>
    </ion-list>
  </ion-content>
</template>

<script>
import { IonContent } from '@ionic/vue';
import { IonItem, IonLabel, IonList, IonSelectOption, IonSelect  } from '@ionic/vue';
import axios from 'axios';


export default {
    name: 'Popover',
    components: {
        IonItem, IonLabel, IonList, IonSelectOption, IonSelect, IonContent
    },
    data () {
        return{
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
        }
    },
    created: function () {
        this.GetInfos();
    },
    beforeUnmount: function () {
        console.log("tmtc");
        this.GetFilteredRessources();
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
        }
        , GetFilteredRessources(){
            this.$store.dispatch("mazRessources");
            const config = {
                params: {
                    monJson: {
                        idCategorie: parseInt(this.categorie),
                        idType: parseInt(this.type),
                        Relations: parseInt(this.relation),
                        idStatut: parseInt(this.statut)
                    }
                },
                headers: { 
                    "Content-Type" : "application/json",
                    "Authorization": `Bearer ${this.$store.getters.utilisateur.token.value}`                     
                }
            }
            console.log(config);
            axios.get(this.$constapi + 'ressources/GetFilteredRessources/' + this.$store.getters.utilisateur.mail, config)
            .then(response => {
                // tout s'est bien passé
               response.data.forEach(ressource => {
                   //console.log("import de : ");
                   //console.log(ressource);
                   this.$store.dispatch("ajouteRessource", { id : ressource.id_ressource, 
                                                            image : ressource.chemin_document,
                                                            title : ressource.titre_ressource,
                                                            description : ressource.description_ressource});
               });
            });
        }
    }
}
</script>