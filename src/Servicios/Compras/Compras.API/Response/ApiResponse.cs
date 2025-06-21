namespace Compras.API.Response
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public static ApiResponse<T> Ok(T data, string message = "") =>
            new() { Success = true, Message = message, Data = data };

        public static ApiResponse<List<string>> Fail(List<string> error, string message = "Errores de validación") =>
            new() { Success = false, Message = message, Data = error };

    }
}
