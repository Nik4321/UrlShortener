using System;
using System.Diagnostics.CodeAnalysis;
using UrlShortener.Data.Models.Entities.Interfaces;

namespace UrlShortener.Data.Models.Entities
{
    [ExcludeFromCodeCoverage]
    public class BaseEntity<TKey> : IAudit
    {
        public TKey Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
