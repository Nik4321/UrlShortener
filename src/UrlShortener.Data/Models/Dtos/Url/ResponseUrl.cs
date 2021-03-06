﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace UrlShortener.Data.Models.Dtos.Url
{
    [ExcludeFromCodeCoverage]
    public class ResponseUrl
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public string ShortUrl { get; set; }

        public string LongUrl { get; set; }
    }
}
