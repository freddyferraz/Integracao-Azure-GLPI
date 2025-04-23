using Integracao.Infra.Abstractions;
using Integracao.Infra.Repositories;
using IntegracaoGLPI_DevOps.Core.Structs;
using IntegracaoGLPI_DevOps.Service.Interfaces;
using IntegracaoGLPI_DEvOps.Service.DTO;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;



namespace IntegracaoGLPI_DEvOps.Service.Services
{
    public class IntegraGLPIService : IIntegraGLPIService
    {
        private HttpClient _httpClient;
        private string _token;
        private string _sessionToken;
        private string url;
        private IDeParaStatusRepository deParaStatusRepository;

        public IntegraGLPIService()
        {
            _httpClient = new HttpClient();
            url = Environment.GetEnvironmentVariable("urlGLPI");
                            
        }
         
        public async Task<Optional<TokenGLPIDTO>> InitSession(string authToken)
        {
            var urltmp = url + "initSession/";

            try
            {
                _httpClient.DefaultRequestHeaders.Add("App-Token", authToken);
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("user_token", System.Environment.GetEnvironmentVariable("user_token"));

                var data = new
                {

                };
                var jsonData = JsonSerializer.Serialize(data);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(urltmp, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();

                    TokenGLPIDTO tokenGLPIDTO = JsonSerializer.Deserialize<TokenGLPIDTO>(responseContent);

                    _sessionToken = tokenGLPIDTO.session_token;


                    return tokenGLPIDTO;
                }
                else
                {

                    return new Optional<TokenGLPIDTO>();
                }

            }
            catch (Exception ex)
            {
                return new Optional<TokenGLPIDTO>();
            }

        }

        public async Task<Optional<bool>> KillSession(string authToken)
        {
            var client = new HttpClient();
            var urltmp = url + "killSession/";

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, urltmp);

                request.Headers.Add("Session-Token", _sessionToken);
                request.Headers.Add("App-Token", authToken);

                var content = new StringContent("", null, "txt/plain");

                request.Content = content;

                var response = await client.SendAsync(request);



                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }


            catch (Exception ex)
            {
                return false;
            }


        }

        public async Task<Optional<RetornoTicketDTO>> UpdateGLPI(string authToken, TicketGLPIDTO ticketGLPIDTO)

        {
            var client = new HttpClient();
            var urltmp = url + $"Ticket/{ticketGLPIDTO.id}/ITILFollowup";
            var request = new HttpRequestMessage(HttpMethod.Post, urltmp);



            request.Headers.Add("Session-Token", _sessionToken);
            request.Headers.Add("App-Token", authToken);



            try
            {


                JObject json = new JObject(
                                new JProperty("input", new JObject(
                                    new JProperty("status", ticketGLPIDTO.status),
                                    new JProperty("items_id", ticketGLPIDTO.items_id),
                                    new JProperty("itemtype", ticketGLPIDTO.itemtype),
                                    new JProperty("content", ticketGLPIDTO.content)
                                ))
                        );



                var jsonData = json.ToString();


                var content = new StringContent(jsonData, null, "application/json");

                request.Content = content;

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var retornoTicketDTO = JsonSerializer.Deserialize<RetornoTicketDTO>(responseContent);
                    return retornoTicketDTO;
                }
                else
                {

                    return new Optional<RetornoTicketDTO>();
                }




            }
            catch (Exception ex)
            {


                return new Optional<RetornoTicketDTO>();
            }
        }

        private async Task<Optional<RetornoTicketDTO>>UpdateStatus(string authToken, TicketGLPIDTO ticketGLPIDTO)
        {
            var client = new HttpClient();
            var urlTmp = url + $"Ticket/{ticketGLPIDTO.id}";
            var request = new HttpRequestMessage(HttpMethod.Post, urlTmp);

            var deParaStatus = await deParaStatusRepository.RetornaTDeParaStatusById(ticketGLPIDTO.status);

            request.Headers.Add("Session-Token", _sessionToken);
            request.Headers.Add("App-Token", authToken);

            JObject json = new JObject(
                                new JProperty("input", new JObject(
                                    new JProperty("items_id", ticketGLPIDTO.items_id),
                                    new JProperty("status", deParaStatus.AcodStatusGlpi)
                                )));

            var jsonData = json.ToString();


            var content = new StringContent(jsonData, null, "application/json");

            request.Content = content;

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var retornoTicketDTO = JsonSerializer.Deserialize<RetornoTicketDTO>(responseContent);
                return retornoTicketDTO;
            }
            else
            {

                return new Optional<RetornoTicketDTO>();
            }

        }

    }
}
