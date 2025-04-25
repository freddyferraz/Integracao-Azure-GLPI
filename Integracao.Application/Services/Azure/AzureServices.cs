using Integracao.Application.Abstractions;
using Integracao.Domain.Errors;
using Integracao.Domain.Shared;
using Integracao.Domain.ValueObjects.Azure;
using System.Text;
using System.Text.Json;

namespace Integracao.Application.Services.Azure;
internal class AzureServices : IAzureServices
{
    public async ValueTask<Result<CardAzureResponse>> RegistraCardAzure(CardAzureRequest request, string operacao, long acodAzure)
    {
        if (operacao == "add")
        {
           return await CreateCardAzure(request, operacao, acodAzure);
        }
        else 
        {
            return await UpdateCardAzure(request, operacao, acodAzure);

        }
    }
    
    
    
    //Métodos Privados para realização de processos na Service
    private async ValueTask<Result<CardAzureResponse>> CreateCardAzure(CardAzureRequest command, string operacao, long idAzure)
    {
        var _httpClient = new HttpClient();
        var _apiKey = Environment.GetEnvironmentVariable("auth_dev_ops");
        var url = Environment.GetEnvironmentVariable("urlDevOps") + $"$CHAMADO%20GLPI/?api-version=7.0";
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, url);

        httpRequest.Headers.Add("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"user:{_apiKey}"))}");



        var targetData = Transform(command);
        var json = JsonSerializer.Serialize(targetData);
        var content = new StringContent(json, null, "application/json-patch+json");
        httpRequest.Content = content;
        var response = await _httpClient.SendAsync(httpRequest);

        if (!response.IsSuccessStatusCode)
        {
            return Result.Failure<CardAzureResponse>(TicketErrors.ErroAtualizarCardAzure);
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        var CardAzureResponse = JsonSerializer.Deserialize<CardAzureResponse>(responseContent);
        return Result.Success(CardAzureResponse);


    }
    private async ValueTask<Result<CardAzureResponse>> UpdateCardAzure(CardAzureRequest command, string operacao, long idAzure)
    {
        var _httpClient = new HttpClient();
        var _apiKey = Environment.GetEnvironmentVariable("auth_dev_ops");
        var url = Environment.GetEnvironmentVariable("urlDevOps") + $"{idAzure}?api-version=7.0" ;
        var httpRequest = new HttpRequestMessage(HttpMethod.Patch, url);

        httpRequest.Headers.Add("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"user:{_apiKey}"))}");
        
       

        var targetData = Transform(command);
        var json = JsonSerializer.Serialize(targetData);
        var content = new StringContent(json, null, "application/json-patch+json");
        httpRequest.Content = content;
        var response = await _httpClient.SendAsync(httpRequest);

        if (!response.IsSuccessStatusCode)
        {
            var responseContents = await response.Content.ReadAsStringAsync();
            return Result.Failure<CardAzureResponse>(TicketErrors.ErroAtualizarCardAzure);
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        var CardAzureResponse = JsonSerializer.Deserialize<CardAzureResponse>(responseContent);
        
        return Result.Success(CardAzureResponse);


    }
    private class TargetData
    {
        public string op { get; set; }
        public string path { get; set; }
        public string value { get; set; }
    }
    private static List<TargetData> Transform(CardAzureRequest request)
    {
        var result = new List<TargetData>();

        // Mapeamento dos campos. Adapte de acordo com a sua necessidade.
        var mapping = new Dictionary<string, string>
        {
            {"Area", "System.AreaPath"},
            {"Title", "System.Title"},
            {"Description", "System.Description"},
            {"History", "System.History"},
            {"Requerente", "Custom.Requerente"},
            {"Observador", "Custom.Observador"},
            {"ResponsavelAtendimento","Custom.e1499902-ec8f-4840-bb42-eba1b9c1d6f0"},
            {"TipoChamado", "Custom.TipodeChamado"},
            {"CaterogiraChamado", "Custom.CategoriadoChamado"},
            {"Prioridade", "Custom.Prioridade"},
            {"Impacto", "Custom.Impacto"},
            {"Urgencia", "Custom.25d8d63f-ed8b-42fc-b72a-754868bbdcac"},
            {"Localizacao", "Custom.fb57db61-fbe4-4ec1-9a1a-822f31903c9a"},
            {"Data", "Custom.c632119c-2c55-43ba-9da6-86e59ab7e5c8"},
            {"Link", "Custom.LinkparaoChamado"},
            {"Status", "System.State"},
            
        };

        foreach(var property in typeof(CardAzureRequest).GetProperties())
        {
            if (mapping.TryGetValue(property.Name, out var targetPath))
            {
                result.Add(new TargetData
                {
                    op = request.Operacao,
                    path = $"/fields/{targetPath}",
                    value = property.GetValue(request)?.ToString()
                });
            }
        }
        return result;
    }
}

