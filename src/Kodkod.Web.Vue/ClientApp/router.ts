import Vue from 'vue';
import VueRouter from 'vue-router';
import Store from './shared/stores/auth-store';

Vue.use(VueRouter);

const accountLayout = require('./account/account-layout.vue.html').default;
const adminLayout = require('./admin/admin-layout.vue.html').default;

const router = new VueRouter({
    mode: 'history',
    routes: [
        { path: '/', redirect: '/admin/home' },
        {
            path: '/account',
            component: accountLayout,
            children: [
                { path: 'login', component: require('./account/views/login/login.vue.html').default },
                { path: 'register', component: require('./account/views/register/register.vue.html').default }
            ]
        },
        {
            path: '/admin',
            component: adminLayout,
            meta: { requiresAuth: true },
            children: [
                { path: 'home', component: require('./admin/views/home/home.vue.html').default },
                { path: 'counter', component: require('./admin/views/counter/counter.vue.html').default },
                { path: 'fetchdata', component: require('./admin/views/fetchdata/fetchdata.vue.html').default },
                { path: 'user-list', component: require('./admin/views/users/user-list.vue.html').default }
            ]
        }
    ]
});

//todo: comment out following lines after implementing authorizations
//router.beforeEach((to: any, from: any, next: any) => {
//    if (to.matched.some((record: any) => record.meta.requiresAuth)) {
//        // this route requires auth, check if logged in
//        // if not, redirect to login page.
//        if (!Store.isSignedInIn()) {
//            next({
//                path: '/account/login',
//                query: { redirect: to.fullPath },
//            });
//        }
//    }
//    next(); // make sure to always call next()!
//});

export default router;