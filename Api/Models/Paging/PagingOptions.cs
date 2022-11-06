using System.ComponentModel.DataAnnotations;

namespace Api.Models.Paging
{
    public class PagingOptions
    {
        [Range(0, 99999, ErrorMessage = "PageIndex must be greater or equal 0.")]
        public int PageIndex { get; set; }
        [Range(1, 100, ErrorMessage = "PageSize must be greater than 0 and less than 100.")]
        public int PageSize { get; set; }
    }
}