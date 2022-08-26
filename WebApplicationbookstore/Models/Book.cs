using System.ComponentModel.DataAnnotations;

namespace WebApplicationbookstore.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Tittle")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Please Enter Information")]
        public string? Info { get; set; }
        [Required(ErrorMessage = "Please Enter Quantity")]
        [Display(Name = "Quantity ")]
        public int Bookquantity { get; set; }
        [Required(ErrorMessage = "Please Enter Price")]
        public int Price { get; set; }
        [Required(ErrorMessage = "Please select Catgory")]
        [Display(Name = "Catgory ")]
        public int Cataid { get; set; }
        [Required(ErrorMessage = "Please Enter Name")]
        [Display(Name = "Author ")]
        public string? Author { get; set; }
        [Required(ErrorMessage = "Please Enter Imgfile")]
        public string? Imgfile { get; set; }

        public virtual ICollection<Comment>? BookComments { get; set; }
    }
}
