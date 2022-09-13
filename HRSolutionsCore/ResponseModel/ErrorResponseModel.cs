namespace HRSolutionsCore.ResponseModel
{
    public class ErrorResponseModel
    {
        public ErrorModel? error { get; set; }
    }
    public class ErrorModel
    {
        public string? code { get; set; }
        public string? message { get; set; }
        public dynamic? innerError { get; set; }
    }
    public class AddUpdateDeleteResponse
    {
        public string Message { get; set; } = string.Empty; 
        public bool Success { get; set; }
        public dynamic? Data { get; set; }
    }
    public class ResponseModel<T1, T2> where T1 :class where T2 : class 
    {
        public T1 Success { get; set; }
        public T2 Error { get; set; }
    }
}
