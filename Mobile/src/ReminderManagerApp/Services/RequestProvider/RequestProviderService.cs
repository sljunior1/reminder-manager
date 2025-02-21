using ReminderManagerApp.Exceptions;
using ReminderManagerApp.Services.RequestProvider;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace ReminderManagerApp.Services;
public class RequestProviderService : IRequestProviderService
{
    private readonly Lazy<HttpClient> _httpClient =
        new(() =>
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            var httpClient = new HttpClient(handler);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return httpClient;
        },
            LazyThreadSafetyMode.ExecutionAndPublication);
    private HttpClient GetOrCreateHttpClient(string token = "")
    {
        var httpClient = _httpClient.Value;

        httpClient.DefaultRequestHeaders.Authorization =
            !string.IsNullOrEmpty(token)
                ? new AuthenticationHeaderValue("Bearer", token)
                : null;

        return httpClient;
    }
    private static async Task HandleResponse(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.Forbidden ||
                    response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new ServiceAuthenticationException(content);
            }

            throw new HttpRequestExceptionEx(response.StatusCode, content);
        }
    }
    public async Task<TResult> GetAsync<TResult>(string uri, string token = "")
    {
        var httpClient = GetOrCreateHttpClient(token);
        var response = await httpClient.GetAsync(uri);

        await HandleResponse(response);

        var result = await response.Content.ReadFromJsonAsync<TResult>();

        return result!;
    }
    public async Task<TResult> PostAsync<TResult, TSend>(string uri, TSend data, string token = "")
    {
        var httpClient = GetOrCreateHttpClient(token);

        var body = new StringContent(JsonSerializer.Serialize(data));
        body.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var response = await httpClient.PostAsync(uri, body);

        var result = await response.Content.ReadFromJsonAsync<TResult>();

        return result!;
    }
    public async Task<TSend> PostAsync<TSend>(string uri, TSend data, string token = "")
    {
        var httpClient = GetOrCreateHttpClient(token);

        var body = new StringContent(JsonSerializer.Serialize(data));
        body.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var response = await httpClient.PostAsync(uri, body);

        var result = await response.Content.ReadFromJsonAsync<TSend>();

        return result!;
    }
    public async Task<TResult> DeleteAsync<TResult>(string uri, string token = "")
    {
        var httpClient = GetOrCreateHttpClient(token);
        var response = await httpClient.DeleteAsync(uri);

        await HandleResponse(response);

        var result = await response.Content.ReadFromJsonAsync<TResult>();

        return result!;
    }
    public async Task<TResult> PatchAsync<TResult, TSend>(string uri, TSend data, string token = "")
    {
        var httpClient = GetOrCreateHttpClient(token);

        var body = new StringContent(JsonSerializer.Serialize(data));
        body.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var response = await httpClient.PatchAsync(uri, body);

        var result = await response.Content.ReadFromJsonAsync<TResult>();

        return result!;
    }
}
