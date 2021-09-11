import { createStore } from 'vuex';
// Fonction pour enlever les caractère spéciaux pour les envoyer dans les urls
export function noSpecialChar(maVar) {
    return maVar.replace(/[^a-zA-Z0-9]/g, '');
}
const store = createStore({
    state() {
        return {
            ressources: [],
            utilisateur: {
                token: 'guest',
                mail: "guest",
            }
        };
    },
    mutations: {
        addressource(state, ressourceData) {
            const newRessource = {
                id: ressourceData.id,
                title: ressourceData.title,
                image: ressourceData.image,
                description: ressourceData.description
            };
            state.ressources.unshift(newRessource);
        },
        changeMonMail(state, newMail) {
            state.utilisateur.mail = newMail;
        },
        changeMonToken(state, newToken) {
            state.utilisateur.token = newToken;
        },
        miseAZeroRessources(state) {
            state.ressources = [];
        },
        ajouteMaRessource(state, newRessource) {
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
        mazRessources(context) {
            context.commit('miseAZeroRessources');
        },
        ajouteRessource(context, newRessource) {
            context.commit('addressource', newRessource);
        }
    },
    getters: {
        ressources(state) {
            return state.ressources;
        },
        ressource(state) {
            return (ressourceId) => {
                return state.ressources.find(ressource => ressource.id == ressourceId);
            };
        },
        utilisateur(state) {
            return state.utilisateur;
        }
    }
});
export default store;
//# sourceMappingURL=index.js.map