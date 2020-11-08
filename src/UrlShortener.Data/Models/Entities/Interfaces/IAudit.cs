using System;

namespace UrlShortener.Data.Models.Entities.Interfaces
{
    public interface IAudit
    {
        DateTime CreatedOn { get; set; }

        DateTime? ModifiedOn { get; set; }
    }
}
