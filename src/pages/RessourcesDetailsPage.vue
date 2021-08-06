<template>
    <base-layout :pageTitle="loadedRessource ? loadedRessource.title : 'Loading...'" page-default-back-link="/catalog">
        <h2 v-if="!loadedRessource">Could not find a ressource for the given id.</h2>
        <ressource-overview v-else :title="loadedRessource.title" :image="loadedRessource.image" :description="loadedRessource.description" :id="ressourceId" ></ressource-overview>
    </base-layout>
</template>

<script>
import RessourceOverview from '../components/ressources/RessourceOverview.vue'
export default {
  components: {
    RessourceOverview
  },
  data() {
    return {
      ressourceId: this.$route.params.id
    }
  },
  computed: {
    loadedRessource() {
      return this.$store.getters.ressource(this.ressourceId);
    }
  },
  // watch: {
  //   '$route'(currentRoute) {
  //     this.ressourceId = currentRoute.params.id;
  //   }
  // }
};
</script>