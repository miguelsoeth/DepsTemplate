using System.Collections.Generic;

namespace DepsTemplate.SharedKernel
{
    public class PagedList<T>
    {
        public int TotalItems { get; set; }
        public List<T> Items { get; set; }
    }
}
