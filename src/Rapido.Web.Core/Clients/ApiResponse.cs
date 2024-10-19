namespace Rapido.Web.Core.Clients;

public class ApiResponse
{
    public HttpResponseMessage HttpResponse { get; }
    public bool Succeeded { get; }

    public ApiResponse(HttpResponseMessage httpResponse, bool succeeded)
    {
        HttpResponse = httpResponse;
        Succeeded = succeeded;
    }
}

public class ApiResponse<T> : ApiResponse
{
    public T Value { get; }
    
    public ApiResponse(T value, HttpResponseMessage httpResponse, bool succeeded)
        : base(httpResponse, succeeded)
    {
        Value = value;
    }
}