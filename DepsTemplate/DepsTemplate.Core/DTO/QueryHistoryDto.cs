using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepsTemplate.Core.DTO
{
    public class QueryHistoryDto
    {
        public string username { get; set; }
        public string type { get; set; }
        public string document { get; set; }
        public DateTime referredDate { get; set; }
        public string interval { get; set; }
        public string? interval_label { get; set; }
    }
}
