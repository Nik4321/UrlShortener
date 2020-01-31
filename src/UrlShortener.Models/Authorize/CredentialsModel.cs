using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Models.Authorize
{
    public class CredentialsModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
