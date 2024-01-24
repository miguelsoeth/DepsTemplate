using DepsTemplate.Core.DTO;
using DepsTemplate.Core.ExternalModels;
using DepsTemplate.Core.Interfaces;
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
        public async Task<ResponseGenerico<List<ExternalPepModelResponse>>> ConsultaPep(string cpf, string periodoInicial, string periodoFinal)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.portaldatransparencia.gov.br/api-de-dados/peps?cpf={cpf}&dataInicioExercicioDe={periodoInicial}&datInicioExercicioAte={periodoFinal}");
            request.Headers.Add("chave-api-dados", apiKey);

            var response = new ResponseGenerico<List<ExternalPepModelResponse>>();
            using (var client = new HttpClient())
            {
                var responsePortalTransparenciaApi = await client.SendAsync(request);
                var contentResponse = await responsePortalTransparenciaApi.Content.ReadAsStringAsync();
                var objResponse = JsonSerializer.Deserialize<List<ExternalPepModelResponse>>(contentResponse);

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

        public async Task<ResponseGenerico<List<ExternalCepimModelResponse>>> ConsultaCepim(string cnpj)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.portaldatransparencia.gov.br/api-de-dados/cepim?cnpjSancionado={cnpj}");
            request.Headers.Add("chave-api-dados", apiKey);
            var response = new ResponseGenerico<List<ExternalCepimModelResponse>>();

            using (var client = new HttpClient())
            {
                var responsePortalTransparenciaApi = await client.SendAsync(request);
                var contentResponse = await responsePortalTransparenciaApi.Content.ReadAsStringAsync();
                var objResponse = JsonSerializer.Deserialize<List<ExternalCepimModelResponse>>(contentResponse);

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
