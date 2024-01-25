using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepsTemplate.Core.ExternalModels
{
    public class QueryHistoryModel
    {
        public int id { get; set; }
        public string username { get; set; }
        public DateTime querydate { get; set; }
        public string type { get; set; }
        public string document { get; set; }
        public DateTime referreddate { get; set; }
        public string interval { get; set; }
        public string interval_label { get; set; }
    }
}
