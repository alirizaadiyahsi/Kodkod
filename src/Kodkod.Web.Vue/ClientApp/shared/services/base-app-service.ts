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
        let isBadRequest = false;
        let body = data === "" ? null : data;
        const headers: { [key: string]: string } = {
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
                isBadRequest = !response.status.toString().startsWith("2");
                console.log(response);
                return response.json();
            })
            .then((responseContent: any) => {
                console.log(responseContent);
                const response: IRestResponse<T> = {
                    isError: isBadRequest,
                    errorContent: isBadRequest ? responseContent : null,
                    content: isBadRequest ? null : responseContent
                };

                return response;
            });
    }
}