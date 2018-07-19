export default class Auth {
    static storageKey: string = "token";

    static getToken() {
        return window.localStorage.getItem(Auth.storageKey);
    }

    static setToken(token: string) {
        window.localStorage.setItem(Auth.storageKey, token);
    }

    static removeToken(): void {
        window.localStorage.removeItem(Auth.storageKey);
    }
}