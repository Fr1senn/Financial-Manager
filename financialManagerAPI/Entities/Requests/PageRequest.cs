using System.ComponentModel.DataAnnotations;

namespace financial_manager.Entities.Requests
{
    public class PageRequest
    {
        [Range(1,99)]
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}
