using DepsTemplate.Core.DTO;
using DepsTemplate.Core.Interfaces;
using DepsTemplate.Core.Models;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DepsTemplate.Core.Rest
{
    public class PortalTransparenciaRest : IPortalTransparenciaAPI
    {
        private readonly string apiKey = "7c137febe8f79a03ffe7a437026f0e05";
        public async Task<ResponseGenerico<List<PepModel>>> ConsultaPep(string cpf, string periodoInicial, string periodoFinal)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.portaldatransparencia.gov.br/api-de-dados/peps?cpf={cpf}&dataInicioExercicioDe={periodoInicial}&datInicioExercicioAte={periodoFinal}");
            request.Headers.Add("chave-api-dados", apiKey);

            var response = new ResponseGenerico<List<PepModel>>();
            using (var client = new HttpClient())
            {
                var responsePortalTransparenciaApi = await client.SendAsync(request);
                var contentResponse = await responsePortalTransparenciaApi.Content.ReadAsStringAsync();
                var objResponse = JsonSerializer.Deserialize<List<PepModel>>(contentResponse);

                if (responsePortalTransparenciaApi.IsSuccessStatusCode)
                {
                    response.CodigoHttp = responsePortalTransparenciaApi.StatusCode;
                    response.DadosRetorno = objResponse;
                }
                else
                {
                    response.CodigoHttp = responsePortalTransparenciaApi.StatusCode;
                    response.ErroRetorno = JsonSerializer.Deserialize<ExpandoObject>(contentResponse);
                }
            }
            return response;
        }
    }
}
