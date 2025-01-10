using IntegracaoGLPI_DevOps.Core.Structs;
using IntegracaoGLPI_DEvOps.Service.DTO;
using IntegracaoGLPI_DEvOps.Service.Interfaces;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace IntegracaoGLPI_DEvOps.Service.Services;
public class IntegraDevOpsService : IIntegraDevOpsService
{
    private HttpClient _httpClient;
    private string _apiKey;
    private string _url;
    private readonly string user = "";

    public IntegraDevOpsService()
    {
        _httpClient = new HttpClient();
        _apiKey = Environment.GetEnvironmentVariable("auth_dev_ops");
        _url = Environment.GetEnvironmentVariable("urlDevOps");
    }

    public async Task<Optional<RetornoDevOpsDTO>> UpdateDevOps(CardDevOpsDTO updateDevOps)
    {
        var client = new HttpClient();
        var  request  = new HttpRequestMessage(HttpMethod.Post, _url);

        request.Headers.Add("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{user}:{_apiKey}"))}");

        var targetData = Transform(updateDevOps);
        var json = JsonSerializer.Serialize(targetData);


        var content = new StringContent(json, null, "application/json-patch+json");

        request.Content = content;

        var response = await client.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();

            var retornoCardDTO = JsonSerializer.Deserialize<RetornoDevOpsDTO>(responseContent);
            return retornoCardDTO;
        }
        else
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            return new Optional<RetornoDevOpsDTO>();
        }

    }

    private class TargetData
    {
        public string op { get; set; }
        public string path { get; set; }
        public string value { get; set; }
    }

    private static List<TargetData> Transform(CardDevOpsDTO updateDevOps)
    {
        var targetData = new List<TargetData>();

        // Mapeamento dos campos. Adapte de acordo com a sua necessidade.
        var mapping = new Dictionary<string, string>
        {
            {"Area", "System.AreaPath"},
            {"Title", "System.Title"},
            {"Description", "System.Description"},
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
            {"Link", "Custom.LinkparaoChamado"}
        };
        foreach(var property in typeof(CardDevOpsDTO).GetProperties())
        {
            if(mapping.TryGetValue(property.Name, out var targetPath))
            {
                targetData.Add(new TargetData
                {
                    op = updateDevOps.Operacao,
                    path = $"/fields/{targetPath}",
                    value = property.GetValue(updateDevOps)?.ToString()
                });
            }
        }
        return targetData;
    }


}
