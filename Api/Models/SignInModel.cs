using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class SignInModel
    {
        [Required]
        public string Email { get; set; }
        [Required, MinLength(8)]
        public string Password { get; set; }
    }
}