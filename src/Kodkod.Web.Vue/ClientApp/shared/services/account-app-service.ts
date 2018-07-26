﻿import BaseAppService from './base-app-service';
import AuthStore from "../stores/auth-store";

export default class AccountAppService extends BaseAppService {
    login(loginViewModel: ILoginViewModel) {
        return this.post<ILoginResult>('/api/account/login', loginViewModel)
            .then((response) => {
                if (!response.isError) {
                    AuthStore.setToken(response.content.token);
                }

                return response;
            });
    }

    logOut(): void {
        AuthStore.removeToken();
    }
}