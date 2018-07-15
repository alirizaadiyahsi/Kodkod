import './assets/sass/site.css';
import 'bootstrap';
import Vue from 'vue';
import VueRouter from 'vue-router';
Vue.use(VueRouter);

const routes = [
    { path: '/', component: require('./views/home/home.vue.html') },
    { path: '/counter', component: require('./views/counter/counter.vue.html') },
    { path: '/fetchdata', component: require('./views/fetchdata/fetchdata.vue.html') }
];

let vue = new Vue({
    el: '#app-root',
    router: new VueRouter({ mode: 'history', routes: routes }),
    render: h => h(require('./components/app/app.vue.html'))
});