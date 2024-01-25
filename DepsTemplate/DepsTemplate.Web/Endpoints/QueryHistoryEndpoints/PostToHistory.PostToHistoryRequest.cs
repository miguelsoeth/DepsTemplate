using System;

namespace DepsTemplate.Web.Endpoints.QueryHistoryEndpoints
{
    public class PostToHistoryRequest
    {
        public const string Route = "historico-pesquisa/";
        public string username { get; set; }
        public string type { get; set; }
        public string document { get; set; }
        public DateTime referredDate { get; set; }
        public string interval { get; set; }
        public string? interval_label { get; set; }
    }
}
