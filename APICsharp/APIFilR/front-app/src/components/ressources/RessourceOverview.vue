<template>
    <Ion-img :src="image" :alt="title"></Ion-img>
    <h2 class="ion-text-center">{{ title }}</h2>
    <p class="ion-text-center">{{ description }}</p>

    <ion-item>
    </ion-item>
    
    <ion-item>
        <ion-label position="floating" >Ajouter un Commentaire :</ion-label>
        <Ion-Textarea v-model="textCommentaire" placeholder="modifiez-moi"> </Ion-Textarea>
        <ion-button  @click="postCommentaire()">Envoyer</ion-button> 
    </ion-item>
    <br/>
    <ion-label v-if="errorMessage != ''" style="color:#C20000">{{errorMessage}}</ion-label>
    <ion-label class="Commentaire-label" >Commentaires :</ion-label>
    <div class="Commentaire-output" v-for="commentaire in commentaires" :key="commentaire"> 
        <h2> {{commentaire.utilisateur}} </h2>
        <p>  {{commentaire.commentaire}} </p>
    </div>

</template>

<script>
import{ IonImg, IonTextarea, IonItem, IonLabel, IonButton } from '@ionic/vue';
import axios from 'axios';

export default {   
    data () {
        return {
            textCommentaire: '',
            errorMessage: '',
            commentaires : []
        }
    },
    props: ['title', 'image', 'description', 'id'],
    components: {
        IonImg, IonTextarea, IonItem, IonLabel, IonButton  
    },
    created() {  
        this.getCommentaires();
    },    
    methods: {
        postCommentaire(){
            if (this.$store.getters.utilisateur.mail === "guest"){
                this.showError("Vous devez vous connecter pour pouvoir commenter une ressource");
                return;
            }
            if (this.textCommentaire === ""){
                this.showError("Veuillez renseigner votre commentaire");
                return;
            }
            const config = {
                headers: { 
                    "Content-Type" : "application/json",
                    "Authorization": `Bearer ${this.$store.getters.utilisateur.token.value}` 
                }
            };
            const json = JSON.stringify({
                    id_ressource: parseInt(this.id),
                    commentaire: this.textCommentaire
            });
            console.log(json);
            console.log(config);
            axios.post(this.$constapi + 'Ressources/PostCommentaire'  + '/' + this.$store.getters.utilisateur.mail,
                json,
                config
            )
            .then(e=>{
                if(e.data.status == "error") {        
                    this.showError(e.data.message);
                }
            })
            .catch(e => {
                console.log(e);
                console.log(e.response.data);
                this.errors.push(e);
            });
            //On actualise les commentaires
            this.getCommentaires();
        },
        showError(error){
            // On affiche l'erreur à l'utilisateur 7 secondes
            this.errorMessage = error;
            setTimeout(function(){ this.errorMessage = ''; }.bind(this), 7000);
        },
        getCommentaires(){
            axios.get( this.$constapi + 'ressources/GetCommentaire/' + this.id )
            .then(response => {
                // tout s'est bien passé
                response.data.forEach(rep => {
                    this.commentaires.push({idUtilisateur: rep.idUtilisateur, utilisateur : rep.utilisateur, commentaire : rep.commentaire})
                });
            });
        }
    }
}
</script>