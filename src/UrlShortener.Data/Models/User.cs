﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Diagnostics.CodeAnalysis;
using UrlShortener.Data.Models.Interfaces;

namespace UrlShortener.Data.Models
{
    [ExcludeFromCodeCoverage]
    public class User : IdentityUser<int>, IAudit
    {
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}