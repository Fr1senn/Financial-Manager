using System.ComponentModel.DataAnnotations;

namespace financial_manager.Entities.Requests
{
    public class PageRequest
    {
        [Range(1, 99)]
        public int Take { get; set; }
        public int Skip { get; set; }

        public Dictionary<string, string?> Filters { get; set; } = new Dictionary<string, string?>();
        public List<SortOption> SortOptions { get; set; } = new List<SortOption>();
    }

    public class SortOption
    {
        public string FieldName { get; set; } = string.Empty;
        public bool Ascending { get; set; } = true;
    }
}
