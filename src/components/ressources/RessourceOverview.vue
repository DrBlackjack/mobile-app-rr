<template>
    <Ion-img :src="image" :alt="title"></Ion-img>
    <h2 class="ion-text-center">{{ title }}</h2>
    <p class="ion-text-center">{{ description }}</p>

    <ion-item>
    </ion-item>
    
    <ion-item>
        <ion-label position="floating" >Ajouter un Commentaire :</ion-label>
        <Ion-Textarea v-model="textCommentaire" placeholder="modifiez-moi"> </Ion-Textarea>
        <ion-button  @click="testfunction()">Envoyer</ion-button> 
    </ion-item>

    <br/>
    <ion-label class="Commentaire-label" >Commentaires :</ion-label>
    <div class="Commentaire-output" v-for="commentaire in commentaires" :key="commentaire"> 
        <h2> {{commentaire.utilisateur}} </h2>
        <p>  {{commentaire.commentaire}} </p>
    </div>

</template>

<script>
import{ IonImg, IonTextarea, IonItem, IonLabel, IonButton } from '@ionic/vue'
export default {   
    data () {
        return {
            textCommentaire: '',
            commentaires : [
                {
                    utilisateur: 'jean castex',
                    commentaire: 'incroyable ! '
                }, {
                    utilisateur: 'jx',
                    commentaire: 'insane ! '
                }, {
                    utilisateur: 'jean leluc',
                    commentaire: 'idem ! '
                }
            ]
        }
    },
    props: ['title', 'image', 'description'],
    components: {
        IonImg, IonTextarea, IonItem, IonLabel, IonButton  
    },
    methods: {
        testfunction() {
            console.log(this.textCommentaire);
        }
    }
}
</script>