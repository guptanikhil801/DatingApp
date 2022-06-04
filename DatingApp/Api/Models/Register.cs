using System.ComponentModel.DataAnnotations;


namespace Api.Models
{
    public class Register
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [RegularExpression("^[A-Za-z]{1,10}[0-9]{1,10}$")]
        public string Password { get; set; }
    }
}
