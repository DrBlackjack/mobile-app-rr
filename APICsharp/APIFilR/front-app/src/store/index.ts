import { createStore } from 'vuex';

interface Ressource {   
    id: string;
    image: string;
    title: string;
    description: string;    
}

interface Utilisateur{
    token: string;
    mail: string;
}

interface Filtres {
    categorie: number;
    type: number;
    relation: number[];
    statut: number;
}

interface StoreType {    
    ressources: Ressource[];
    utilisateur: Utilisateur;

}

// Fonction pour enlever les caractère spéciaux pour les envoyer dans les urls
export function noSpecialChar(maVar: string){
    return maVar.replace(/[^a-zA-Z0-9]/g, ''); 
}

const store = createStore({    
    state(): StoreType {
        return {
            ressources:  [],
            utilisateur: { 
                token: 'guest',
                mail: "guest",
            }     
        }
    },
    mutations: {
        addressource(state: StoreType, ressourceData) {
            const newRessource = {
                id: ressourceData.id,
                title: ressourceData.title,
                image: ressourceData.image,
                description: ressourceData.description
            };
            state.ressources.unshift(newRessource);
        },
        changeMonMail(state: StoreType, newMail: string) {
            state.utilisateur.mail = newMail;
        },
        changeMonToken(state: StoreType, newToken: string) {
            state.utilisateur.token = newToken;
        },
        miseAZeroRessources(state: StoreType) {
            state.ressources =  [] ;
        },
        ajouteMaRessource(state: StoreType, newRessource: Ressource) {
            state.ressources.push(newRessource);
        }
    },
    actions: {
        changeMail(context, newMail) {
            context.commit('changeMonMail', newMail);
        },        
        changeToken(context, newToken) {
            context.commit('changeMonToken', newToken);
        },
        mazRessources(context){
            context.commit('miseAZeroRessources');
        },
        ajouteRessource(context, newRessource) {
            context.commit('addressource', newRessource);
        }
    },
    getters: {
        ressources(state: StoreType) {
            return state.ressources;
        },
        ressource(state) { 
            return (ressourceId: string) => {
                return state.ressources.find(ressource => ressource.id == ressourceId);
            };
        },        
        utilisateur(state: StoreType) {
            return state.utilisateur;
        }         
    }
});



export default store;

