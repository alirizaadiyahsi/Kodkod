import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import AccountAppService from '../../../shared/services/account-app-service';

@Component
export default class LoginComponent extends Vue {

    username = "";
    password = "";
    error: string = "";

    onSubmit() {
        const accountAppService = new AccountAppService();
        const loginViewModel: ILoginViewModel = { userName: this.username, password: this.password };

        accountAppService.login(loginViewModel).then(response => {
            if (!response.isError) {
                this.$router.push({ path: '/admin/home' });
            } else {
                this.error = response.errorContent == undefined ? "" : response.errorContent.errorDescription;
            }
        });
    }
}