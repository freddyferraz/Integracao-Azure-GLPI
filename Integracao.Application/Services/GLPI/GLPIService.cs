using Integracao.Application.Abstractions;
using Integracao.Domain.Errors;
using Integracao.Domain.Shared;
using Integracao.Domain.ValueObjects.DTOs;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace Integracao.Application.Services.GLPI;
internal class GLPIService : IGLPIService
{
    public async ValueTask<Result<string>> IniciarSessaoGLPI(string authToken)
    {
        var _httpClient = new HttpClient();
        var urltmp = Environment.GetEnvironmentVariable("urlGLPI") + $"initSession/";

        _httpClient.DefaultRequestHeaders.Add("App-Token", authToken);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("user_token", System.Environment.GetEnvironmentVariable("user_token"));

        var data = new
        {

        };
        var jsonData = JsonSerializer.Serialize(data);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(urltmp, content);

        if (!response.IsSuccessStatusCode)
        {
            return Result.Failure<string>(Error.ErroIniciarSessaoGLPI);
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        var tokenGLPIDTO = JsonSerializer.Deserialize<GLPISessionTokenResponse>(responseContent);
        var sessionToken = tokenGLPIDTO.sessionToken;

        return sessionToken!;

    }
    public async ValueTask<Result<RetornoTicketGlpiResponse>> AtualizaStatusGLPI(long acodGlpi, int status, string sessionToken, string authToken)
    {
        var _httpClient = new HttpClient();
        var urltmp = Environment.GetEnvironmentVariable("urlGLPI") + $"Ticket/{acodGlpi}";
        var request = new HttpRequestMessage(HttpMethod.Post, urltmp);

        request.Headers.Add("Session-Token", sessionToken);
        request.Headers.Add("App-Token", authToken);

        JObject json = new JObject(
                                new JProperty("input", new JObject(
                                    new JProperty("status", status),
                                    new JProperty("items_id", acodGlpi)
                                )));

        var jsonData = json.ToString();
        var content = new StringContent(jsonData, null, "application/json");

        request.Content = content;

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            return Result.Failure<RetornoTicketGlpiResponse>(TicketErrors.ErroAtualizarTicketGLPI);
        }
        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<RetornoTicketGlpiResponse>(responseContent);

        return Result.Success(result);


    }
    public async ValueTask<Result<RetornoTicketGlpiResponse>> AtualizaTicketGLPI(long acodGlpi, string comentario, string sessionToken, string authToken)
    {
        var _httpClient = new HttpClient();
        var urltmp = Environment.GetEnvironmentVariable("urlGLPI") + $"Ticket/{acodGlpi}/ITILFollowup";
        var request = new HttpRequestMessage(HttpMethod.Post, urltmp);

        request.Headers.Add("Session-Token", sessionToken);
        request.Headers.Add("App-Token", authToken);

        JObject json = new JObject(
                                new JProperty("input", new JObject(
                                    new JProperty("items_id", acodGlpi),
                                    new JProperty("itemtype", "Ticket"),
                                    new JProperty("content", comentario)
                                )));

        var jsonData = json.ToString();
        var content = new StringContent(jsonData, null, "application/json");

        request.Content = content;

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            return Result.Failure<RetornoTicketGlpiResponse>(TicketErrors.ErroAtualizarTicketGLPI);
        }
        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<RetornoTicketGlpiResponse>(responseContent);

        return Result.Success(result);


    }
    public async ValueTask<bool> FinalizarSessaoGLPI(string authToken, string sessionToken)
    {
        var _httpClient = new HttpClient();
        var urltmp = Environment.GetEnvironmentVariable("urlGLPI") + $"killSession/";
        var request = new HttpRequestMessage(HttpMethod.Delete, urltmp);


        request.Headers.Add("Session-Token", sessionToken);
        request.Headers.Add("App-Token", authToken);

        var content = new StringContent("", null, "txt/plain");

        request.Content = content;

        var response = await _httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }

        return true;
    }

}
