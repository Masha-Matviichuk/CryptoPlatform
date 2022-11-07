using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class MessageModel
    {
        [Required]
        public string EmailFrom { get; set; }
        public string EmailTo { get; set; }
        [Required, MinLength(3), MaxLength(30)]
        public string Subject { get; set; }
        [Required, MinLength(3), MaxLength(500)]
        public string Body { get; set; }
    }
}