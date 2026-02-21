using System.ComponentModel.DataAnnotations;

namespace RAD302Week3Lab12026WebAPIS00236888.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
