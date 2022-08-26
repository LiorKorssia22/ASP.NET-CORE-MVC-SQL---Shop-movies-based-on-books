using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationbookstore.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Display(Name = "Animal Id ")]

        public int BookId { get; set; }
        [ForeignKey("BookId")]

        [Required(ErrorMessage = "Please Enter a Comment")]
        public string? CommentText { get; set; }

        public virtual Book? BookComment { get; set; }
    }
}
