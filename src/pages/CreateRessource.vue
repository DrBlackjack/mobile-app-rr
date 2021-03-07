<template>
    <base-layout pageTitle="Création" page-default-back-link="/start">
        <form class="ion-padding" @submit.prevent="submitForm">
            <ion-list>
                <ion-item>
                    <ion-label position="floating">Titre</ion-label>
                    <ion-input type="text" required />
                </ion-item>
                <ion-item>
                    <ion-label position="floating">Écrivez votre texte ici</ion-label>
                    <ion-textarea rows="5" required></ion-textarea>
                </ion-item>
            </ion-list>
            <ion-button id="outline-button" fill="outline" shape="round" expand="block">Ajouter une image</ion-button>
            <ion-item>
                <ion-label>Catégorie</ion-label>
                <ion-select ok-text="Okay" cancel-text="Nah">
                    <ion-select-option value="dos">DOS</ion-select-option>
                    <ion-select-option value="lunix">Linux</ion-select-option>
                    <ion-select-option value="mac7">Mac OS 7</ion-select-option>
                    <ion-select-option value="mac8">Mac OS 8</ion-select-option>
                    <ion-select-option value="win3.1">Windows 3.1</ion-select-option>
                    <ion-select-option value="win95">Windows 95</ion-select-option>
                    <ion-select-option value="win98">Windows 98</ion-select-option>
                </ion-select>
            </ion-item>
            <ion-item>
                <ion-label>Ressource</ion-label>
                <ion-select ok-text="Okay" cancel-text="Nah">
                    <ion-select-option value="dos">DOS</ion-select-option>
                    <ion-select-option value="lunix">Linux</ion-select-option>
                    <ion-select-option value="mac7">Mac OS 7</ion-select-option>
                    <ion-select-option value="mac8">Mac OS 8</ion-select-option>
                    <ion-select-option value="win3.1">Windows 3.1</ion-select-option>
                    <ion-select-option value="win95">Windows 95</ion-select-option>
                    <ion-select-option value="win98">Windows 98</ion-select-option>
                </ion-select>
            </ion-item>
            <ion-item>
                <ion-label>Relation</ion-label>
                <ion-select ok-text="Okay" cancel-text="Nah">
                    <ion-select-option value="dos">DOS</ion-select-option>
                    <ion-select-option value="lunix">Linux</ion-select-option>
                    <ion-select-option value="mac7">Mac OS 7</ion-select-option>
                    <ion-select-option value="mac8">Mac OS 8</ion-select-option>
                    <ion-select-option value="win3.1">Windows 3.1</ion-select-option>
                    <ion-select-option value="win95">Windows 95</ion-select-option>
                    <ion-select-option value="win98">Windows 98</ion-select-option>
                </ion-select>
            </ion-item>
            <ion-button shape="round" type="submit" expand="block">Créer</ion-button>
        </form>
    </base-layout>
</template>

<script>
export default {
    
}
</script>