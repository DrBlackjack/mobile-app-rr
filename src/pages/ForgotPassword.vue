<template>
    <base-layout pageTitle="Mot de passe oublié" page-default-back-link="/start">
        <form class="ion-padding" @submit.prevent="submitForm">
            <ion-list>
                <ion-item>
                    <ion-label position="floating">Email</ion-label>
                    <ion-input type="email" required v-model="email" />
                </ion-item>
            </ion-list>
            <ion-button @click="testfunction()" shape="round" type="submit" expand="block">Envoyer un mail</ion-button>
        </form>
    </base-layout>
</template>

<script>

import{ IonList, IonItem, IonButton, IonLabel, IonInput } from '@ionic/vue';
export default {    
    components: {  
        IonList, IonItem, IonButton, IonLabel, IonInput
    },
    data () {
        return{
            email:''
        }
    },
    methods: {
        testfunction() {
            console.log("this.email");
            console.log(this.email);
        }
    }
}
</script>