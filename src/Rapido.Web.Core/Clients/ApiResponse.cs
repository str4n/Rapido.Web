namespace Rapido.Web.Core.Clients;

public class ApiResponse
{
    public HttpResponseMessage HttpResponse { get; }
    public bool Succeeded { get; }
    public Error? Error { get; }

    public ApiResponse(HttpResponseMessage httpResponse, bool succeeded, Error? error = null)
    {
        HttpResponse = httpResponse;
        Succeeded = succeeded;
        Error = error;
    }
}

public sealed class ApiResponse<T> : ApiResponse
{
    public T Value { get; }
    
    public ApiResponse(T value, HttpResponseMessage httpResponse, bool succeeded, Error? error = null)
        : base(httpResponse, succeeded, error)
    {
        Value = value;
    }
}

public sealed record ErrorResponse(int StatusCode, Error Error);

public sealed record Error(string Code, string Reason);