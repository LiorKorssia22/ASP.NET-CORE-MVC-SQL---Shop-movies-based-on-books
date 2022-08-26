using System.ComponentModel.DataAnnotations;

namespace WebApplicationbookstore.Models
{
    public class Report
    {
        public int Id { get; set; }
        [Display(Name = "Customer Name ")]
        public string? Customername { get; set; }
        public int Total { get; set; }

    }
}
