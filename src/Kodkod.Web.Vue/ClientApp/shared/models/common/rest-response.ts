interface IRestResponse<T> {
    isError?: boolean;
    errorMessage: string,
    content?: T
};