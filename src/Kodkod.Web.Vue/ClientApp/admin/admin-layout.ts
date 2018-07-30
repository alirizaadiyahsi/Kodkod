import Vue from 'vue';
import { Component } from 'vue-property-decorator';

@Component({
    components: {
        NavMenuComponent: require('./components/nav-menu/nav-menu.vue.html').default,
        TopMenuComponent: require('./components/top-menu/top-menu.vue.html').default
    }
})
export default class AdminLayoutComponent extends Vue {
}