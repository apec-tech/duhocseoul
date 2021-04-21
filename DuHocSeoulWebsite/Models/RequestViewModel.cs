using System.ComponentModel.DataAnnotations;

namespace DuHocSeoulWebsite.Models
{
    public class RequestViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
