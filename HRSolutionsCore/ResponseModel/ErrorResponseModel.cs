namespace HRSolutionsCore.ResponseModel
{
    public class errorResponseModel
    {
        public errorModel? error { get; set; }
    }
    public class errorModel
    {
        public string? code { get; set; }
        public string? message { get; set; }
        public bool Success { get; set; } = true;
        public dynamic? innerError { get; set; }
    }
    public class addUpdateDeleteResponse
    {
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; } = false;
        public dynamic? Data { get; set; }
    }
    public class responseModel<T1, T2> where T1 : class where T2 : class
    {
        public T1? successResponse { get; set; }
        public T2? errorResponse { get; set; }
    }
}
