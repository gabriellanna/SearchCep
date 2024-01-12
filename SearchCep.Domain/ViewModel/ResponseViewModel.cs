namespace SearchCep.Domain.ViewModel
{
    public class ResponseViewModel
    {
        public ResponseViewModel(bool success, string? message, object? data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public bool Success { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
    }
    public class ResponseViewModel<T>
    {
        public ResponseViewModel(bool success, string? message, T? data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}