<template>
    <base-layout pageTitle="Mot de passe oublié" page-default-back-link="/start">
        <form class="ion-padding" @submit.prevent="submitForm">
            <ion-list>
                <ion-item>
                    <ion-label position="floating">Email</ion-label>
                    <ion-input type="email" required />
                </ion-item>
            </ion-list>
            <ion-button shape="round" type="submit" expand="block">Envoyer un mail</ion-button>
        </form>
    </base-layout>
</template>

<script>
export default {
    
}
</script>