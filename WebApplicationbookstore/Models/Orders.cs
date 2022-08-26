using System.ComponentModel.DataAnnotations;

namespace WebApplicationbookstore.Models
{
    public class Orders
    {
        public int Id { get; set; }
        [Display(Name = "Book Id ")]
        public int BookId { get; set; }
        [Display(Name = "User Id ")]
        public int Userid { get; set; }
        public int Quantity { get; set; }
        [Display(Name = "Order Date ")]
        public DateTime Orderdate { get; set; }

    }
}
