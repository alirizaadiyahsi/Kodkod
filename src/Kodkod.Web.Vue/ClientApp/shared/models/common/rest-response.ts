interface IRestResponse<T> {
    isError?: boolean;
    errorContent?: IErrorContent,
    content?: T
};