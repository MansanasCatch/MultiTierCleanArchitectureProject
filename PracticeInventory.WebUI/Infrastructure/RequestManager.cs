using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace PracticeInventory.WebUI.Infrastructure;

public class RequestManager
{
    public static void SetTokenCookie(IHttpContextAccessor httpContext, string token)
    {
        httpContext.HttpContext?.Response.Cookies.Append("token", token, new CookieOptions { Expires = DateTime.Now.AddMinutes(1) });
    }
    public static string? GetToken(IHttpContextAccessor httpContext)
    {
        var token = String.Empty;
        if (httpContext is not null)
        {
            token = httpContext?.HttpContext?.Request.Cookies["token"];
        }
        return token;
    }
    public static void ClearAllCookies(IHttpContextAccessor httpContext)
    {
        if (httpContext.HttpContext is not null)
        {
            foreach (var cookie in httpContext.HttpContext.Request.Cookies.Keys)
            {
                httpContext.HttpContext.Response.Cookies.Delete(cookie);
            }
        }
    }
    public static async Task<HttpResponseMessage> PostRequestAnonymous(IConfiguration config, string requestUrl, dynamic payload)
    {
        var httpClient = new HttpClient();
        var baseUrl = config.GetValue<string>(AgentConfig.BaseAddress);
        httpClient.BaseAddress = new Uri(baseUrl ?? "");
        HttpContent? _httpContent = null;
        if (payload is not null)
        {
            var stringPayload = JsonConvert.SerializeObject(payload);
            _httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
        }
        var httpResponse = await httpClient.PostAsync(requestUrl, _httpContent);
        return httpResponse;
    }
    public static async Task<HttpResponseMessage> PostRequest(IConfiguration config, IHttpContextAccessor httpContextAccessor, string requestUrl, dynamic? payload)
    {
        var token = GetToken(httpContextAccessor);
        var baseUrl = config.GetValue<string>(AgentConfig.BaseAddress);
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(baseUrl ?? "");
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        HttpContent? _httpContent = null;
        if (payload is not null)
        {
            var stringPayload = JsonConvert.SerializeObject(payload);
            _httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
        }
        var httpResponse = await httpClient.PostAsync(requestUrl, _httpContent);
        return httpResponse;
    }
    public static async Task<HttpResponseMessage> PutRequest(IConfiguration config, IHttpContextAccessor httpContextAccessor, string requestUrl, dynamic? payload)
    {
        var token = GetToken(httpContextAccessor);
        var baseUrl = config.GetValue<string>(AgentConfig.BaseAddress);
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(baseUrl ?? "");
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        HttpContent? _httpContent = null;
        if (payload is not null)
        {
            var stringPayload = JsonConvert.SerializeObject(payload);
            _httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
        }
        var httpResponse = await httpClient.PutAsync(requestUrl, _httpContent);
        return httpResponse;
    }
    public static async Task<HttpResponseMessage> DeleteRequest(IConfiguration config, IHttpContextAccessor httpContextAccessor, string requestUrl)
    {
        var token = GetToken(httpContextAccessor);
        var baseUrl = config.GetValue<string>(AgentConfig.BaseAddress);
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(baseUrl ?? "");
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var httpResponse = await httpClient.DeleteAsync(requestUrl);
        return httpResponse;
    }
    public static async Task<HttpResponseMessage> GetRequest(IConfiguration config, IHttpContextAccessor httpContextAccessor, string requestUrl)
    {
        var token = GetToken(httpContextAccessor);
        var baseUrl = config.GetValue<string>(AgentConfig.BaseAddress);
        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var baseAddress = new Uri(baseUrl ?? "");
        var uri = new Uri(baseAddress + requestUrl);
        var response = await client.GetAsync(uri).ConfigureAwait(false);
        return response;
    }
}