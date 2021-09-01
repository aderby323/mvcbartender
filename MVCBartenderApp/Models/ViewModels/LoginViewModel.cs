
using System.ComponentModel.DataAnnotations;

namespace MVCBartenderApp.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(25, ErrorMessage = "Username must not be more than 25 characters.")]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
