using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Register
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
