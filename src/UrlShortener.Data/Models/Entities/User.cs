using Microsoft.AspNetCore.Identity;
using System;
using System.Diagnostics.CodeAnalysis;
using UrlShortener.Data.Models.Entities.Interfaces;

namespace UrlShortener.Data.Models.Entities
{
    [ExcludeFromCodeCoverage]
    public class User : IdentityUser<int>, IAudit
    {
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}