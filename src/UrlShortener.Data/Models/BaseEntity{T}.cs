using System;
using System.Diagnostics.CodeAnalysis;
using UrlShortener.Data.Models.Interfaces;

namespace UrlShortener.Data.Models
{
    [ExcludeFromCodeCoverage]
    public class BaseEntity<TKey> : IAudit
    {
        public TKey Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
