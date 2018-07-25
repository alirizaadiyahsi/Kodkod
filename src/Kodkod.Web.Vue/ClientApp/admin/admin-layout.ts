import Vue from 'vue';
import { Component } from 'vue-property-decorator';

@Component({
    components: {
        MenuComponent: require('./components/nav-menu/nav-menu.vue.html').default
    }
})
export default class AdminLayoutComponent extends Vue {
}