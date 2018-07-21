import AuthStore from '../stores/auth-store';

export default class BaseAppService {
    //todo: get this from a config file
    static baseApiUrl = "http://localhost:60320";

    get<T>(url: string): Promise<IRestResponse<T>> {
        return BaseAppService.request<T>('GET', url);
    }

    delete(url: string): Promise<IRestResponse<void>> {
        return BaseAppService.request<void>('DELETE', url);
    }

    put<T>(url: string, data: Object | string): Promise<IRestResponse<T>> {
        return BaseAppService.request<T>('PUT', url, data);
    }

    post<T>(url: string, data: Object | string): Promise<IRestResponse<T>> {
        return BaseAppService.request<T>('POST', url, data);
    }

    private static request<T>(method: string, url: string, data: Object | string = ""): Promise<IRestResponse<T>> {
        let isJsonResponse = false;
        let isBadRequest = false;
        let body = data === "" ? null : data;
        let headers: { [key: string]: string } = {
            'Authorization': `Bearer ${AuthStore.getToken()}`
        };

        if (data) {
            body = JSON.stringify(data);
        }

        return fetch(this.baseApiUrl + url,
            ({
                method: method,
                headers: headers,
                body: body
            }) as any)
            .then((response) => {
                if (response.status === 401) {
                    // Unauthorized; redirect to sign-in
                    AuthStore.removeToken();
                    window.location.replace(`/?expired=1`);
                }

                isBadRequest = !response.status.toString().startsWith("2");

                let responseContentType = response.headers.get("content-type");
                if (responseContentType && responseContentType.indexOf("application/json") !== -1) {
                    isJsonResponse = true;
                    return response.json();
                } else {
                    return response.text();
                }
            })
            .then((responseContent: any) => {
                let response: IRestResponse<T> = {
                    isError: isBadRequest,
                    errorContent: isBadRequest ? responseContent : null,
                    content: isBadRequest ? null : responseContent
                };
                return response;
            });
    }
}