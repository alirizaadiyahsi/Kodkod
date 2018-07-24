import './assets/sass/site.css';
import 'bootstrap';
import router from './router';
import Vue from 'vue';

let vue = new Vue({
    el: '#app-root',
    router: router,
    render: h => h(require('./main.vue.html').default)
});