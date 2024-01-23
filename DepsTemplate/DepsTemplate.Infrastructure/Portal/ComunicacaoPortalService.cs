using DepsTemplate.Core.Exceptions;
using DepsTemplate.SharedKernel.Extensions;
using DepsTemplate.SharedKernel.Util;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DepsTemplate.Infrastructure.Portal
{
    public interface IComunicacaoPortalService
    {
        Task<List<Guid>> ObterCategoriasProduto(Guid produtoId, string token);
    }

    public class ComunicacaoPortalService : IComunicacaoPortalService
    {


        public async Task<List<Guid>> ObterCategoriasProduto(Guid produtoId, string token)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            var httpClient = new HttpClient(clientHandler);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var endpointPortal = AmbienteUtil.GetValue("PORTAL_API");
            var response = await httpClient.GetAsync($"{endpointPortal}/v1/produto/obter-categorias?produtoId={produtoId}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return json.FromJson<List<Guid>>();
            }

            throw new DepsTemplateException("Erro ao comunicar com o portal DEPS");
        }
    }
}
