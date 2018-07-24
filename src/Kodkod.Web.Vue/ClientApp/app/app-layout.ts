import Vue from 'vue';
import { Component } from 'vue-property-decorator';

@Component({
    components: {
        MenuComponent: require('./components/navmenu/navmenu.vue.html').default
    }
})
export default class AdminLayoutComponent extends Vue {
}