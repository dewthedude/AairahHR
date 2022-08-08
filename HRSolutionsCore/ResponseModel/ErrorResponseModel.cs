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
        public string? Message { get; set; }
        public bool Success { get; set; }
        public dynamic? Data { get; set; }
    }
}
