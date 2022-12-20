

using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "User name requried")]
        public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password requried")]
        public string Password { get; set; } = string.Empty;
    }
}
