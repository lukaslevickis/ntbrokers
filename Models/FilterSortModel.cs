using System.Collections.Generic;

namespace NTBrokers.Models
{
    public class FilterSortModel
    {
        public string FilterCity { get; set; }
        public string SortOrder { get; set; }
        public List<string> SortProperties { get; set; } = new() { "Name", "Surname" };
    }
}
