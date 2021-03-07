import { useContext } from 'vue';
import { createStore } from 'vuex';

interface Ressource {
    ressources: {
        id: string;
        image: string;
        title: string;
        description: string;
    }[];
}

const store = createStore({
    state(): Ressource {
        return {
            ressources:  [
                { 
                    id: 'm1',
                    image: "https://img.redbull.com/images/c_limit,w_1500,h_1000,f_auto,q_auto/redbullcom/2018/07/24/04d0b56c-2221-4d93-95fa-40a2a54aff27/gta-v-mont-chiliad-secret",
                    title: "A trip into the mountains",
                    description: "It was a really nice trip!"
                },
                { 
                    id: 'm2',
                    image: "https://i.ytimg.com/vi/gfk-QGCLOxQ/maxresdefault.jpg",
                    title: "Surfing the sea side",
                    description: "Feeling good breeze"
                },
                {
                    id: 'm3',
                    image: "https://nerdskitchen.pl/wp-content/uploads/2016/01/balls-and-rings-cluckin-bell-gta-iv-1.jpg",
                    title: "Good eating",
                    description: "Really tasty"
                },
                { 
                    id: 'm4',
                    image: "https://img.redbull.com/images/c_limit,w_1500,h_1000,f_auto,q_auto/redbullcom/2018/07/24/04d0b56c-2221-4d93-95fa-40a2a54aff27/gta-v-mont-chiliad-secret",
                    title: "A trip into the mountains",
                    description: "It was a really nice trip!"
                },
                { 
                    id: 'm5',
                    image: "https://i.ytimg.com/vi/gfk-QGCLOxQ/maxresdefault.jpg",
                    title: "Surfing the sea side",
                    description: "Feeling good breeze"
                },
                {
                    id: 'm6',
                    image: "https://nerdskitchen.pl/wp-content/uploads/2016/01/balls-and-rings-cluckin-bell-gta-iv-1.jpg",
                    title: "Good eating",
                    description: "Really tasty"
                },
                { 
                    id: 'm7',
                    image: "https://img.redbull.com/images/c_limit,w_1500,h_1000,f_auto,q_auto/redbullcom/2018/07/24/04d0b56c-2221-4d93-95fa-40a2a54aff27/gta-v-mont-chiliad-secret",
                    title: "A trip into the mountains",
                    description: "It was a really nice trip!"
                },
                { 
                    id: 'm8',
                    image: "https://i.ytimg.com/vi/gfk-QGCLOxQ/maxresdefault.jpg",
                    title: "Surfing the sea side",
                    description: "Feeling good breeze"
                },
                {
                    id: 'm9',
                    image: "https://nerdskitchen.pl/wp-content/uploads/2016/01/balls-and-rings-cluckin-bell-gta-iv-1.jpg",
                    title: "Good eating",
                    description: "Really tasty"
                },
                { 
                    id: 'm10',
                    image: "https://img.redbull.com/images/c_limit,w_1500,h_1000,f_auto,q_auto/redbullcom/2018/07/24/04d0b56c-2221-4d93-95fa-40a2a54aff27/gta-v-mont-chiliad-secret",
                    title: "A trip into the mountains",
                    description: "It was a really nice trip!"
                },
                { 
                    id: 'm11',
                    image: "https://i.ytimg.com/vi/gfk-QGCLOxQ/maxresdefault.jpg",
                    title: "Surfing the sea side",
                    description: "Feeling good breeze"
                },
                {
                    id: 'm12',
                    image: "https://nerdskitchen.pl/wp-content/uploads/2016/01/balls-and-rings-cluckin-bell-gta-iv-1.jpg",
                    title: "Good eating",
                    description: "Really tasty"
                }
            ]
        }
    },
    mutations: {
        addressource(state: Ressource, ressourceData) {
            const newRessource = {
                id: new Date().toISOString(),
                title: ressourceData.title,
                image: ressourceData.imageUrl,
                description: ressourceData.description
            };

            state.ressources.unshift(newRessource);
        }
    },
    actions: {
        addressource(context, ressourceData) {
            context.commit('addressource', ressourceData);
        }
    },
    getters: {
        ressources(state: Ressource) {
            return state.ressources;
        },
        ressource(state) {
            return (ressourceId: string) => {
                return state.ressources.find(ressource => ressource.id === ressourceId);
            };
        }
    }
});

export default store;