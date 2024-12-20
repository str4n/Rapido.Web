﻿using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Rapido.Web.Core.Storage;
using Rapido.Web.Core.Users.Models;

namespace Rapido.Web.Core.Clients;

internal sealed class CustomHttpClient : IHttpClient
{
    private readonly HttpClient _client;
    private readonly ILocalStorageService _localStorageService;
    private readonly ILogger<CustomHttpClient> _logger;

    public CustomHttpClient(HttpClient client, ILocalStorageService localStorageService,
        ILogger<CustomHttpClient> logger)
    {
        _client = client;
        _localStorageService = localStorageService;
        _logger = logger;
    }

    public Task<ApiResponse<T?>> GetAsync<T>(string endpoint)
        => TryRequestAsync<T>(new HttpRequestMessage(HttpMethod.Get, endpoint));
    
    public async Task<ApiResponse> PostAsync(string endpoint, object request)
    {
        var response = await TryRequestAsync<object>(new HttpRequestMessage(HttpMethod.Post, endpoint)
        {
            Content = GetPayload(request)
        });
            
        return response;
    }

    public async Task<ApiResponse<T?>> PostAsync<T>(string endpoint, object request)
    {
        var response = await TryRequestAsync<T>(new HttpRequestMessage(HttpMethod.Post, endpoint)
        {
            Content = GetPayload(request)
        });
            
        return response;
    }

    public async Task<ApiResponse> PutAsync(string endpoint, object request)
    {
        var response = await TryRequestAsync<object>(new HttpRequestMessage(HttpMethod.Put, endpoint)
        {
            Content = GetPayload(request)
        });
            
        return response;
    }

    public async Task<ApiResponse> DeleteAsync(string endpoint)
    {
        var response = await TryRequestAsync<object>(new HttpRequestMessage(HttpMethod.Delete, endpoint));

        return response;
    }
    
    private static StringContent GetPayload<T>(T request) 
        => new(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
    
    private async Task<ApiResponse<T?>> TryRequestAsync<T>(HttpRequestMessage request)
    {
        HttpResponseMessage? response = null;
        
        try
        {
            var user = await _localStorageService.GetAsync<User>("user");

            if (user is not null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", user.AccessToken);
            }
            
            var requestId = Guid.NewGuid().ToString("N");
            
            _logger.LogInformation($"Sending HTTP request [ID: {requestId}]...", requestId);
            
            response = await _client.SendAsync(request);
            var isValid = response.IsSuccessStatusCode;
            
            var responseStatus = isValid ? "valid" : "invalid";
            _logger.LogInformation($"Received the {responseStatus} response [ID: {requestId}].", responseStatus, requestId);
            
            var content = await response.Content.ReadAsStringAsync();
            if (!isValid)
            {
                var errorResponse = string.IsNullOrWhiteSpace(content)
                    ? default
                    : JsonSerializer.Deserialize<ErrorResponse>(content, SerializerOptions);

                var error = errorResponse?.Error;
                
                _logger.LogError(response.ToString());
                _logger.LogError(content);
                return new ApiResponse<T?>(default, response, false, error);
            }

            var result = string.IsNullOrWhiteSpace(content)
                ? default
                : JsonSerializer.Deserialize<T>(content, SerializerOptions);

            return new ApiResponse<T?>(result, response, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            
            return new ApiResponse<T?>(default, response!, false);
        }
    }
    
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };
}