using System.ComponentModel.DataAnnotations;

namespace WebApplicationbookstore.Models
{
    public class Usersaccounts
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter User Name")]
        [Display(Name = "User Name ")]
        public string? Name { get; set; }
        [System.ComponentModel.DataAnnotations.Display(Name = "Password ")]
        [Required(ErrorMessage = "Please Enter Password")]
        public string? Pass { get; set; }
        public string? Role { get; set; }
        [Required(ErrorMessage = "Please Enter Email")]
        public string? Email { get; set; }
    }
}