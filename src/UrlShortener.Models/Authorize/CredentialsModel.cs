﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace UrlShortener.Models.Authorize
{
    [ExcludeFromCodeCoverage]
    public class CredentialsModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
