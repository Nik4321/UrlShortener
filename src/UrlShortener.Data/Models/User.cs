using System;
using Microsoft.AspNetCore.Identity;
using UrlShortener.Data.Models.Interfaces;

namespace UrlShortener.Data.Models
{
    public class User : IdentityUser<int>, IAudit
    {
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}