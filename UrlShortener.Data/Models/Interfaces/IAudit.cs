using System;

namespace UrlShortener.Data.Models.Interfaces
{
    public interface IAudit
    {
        DateTime CreatedOn { get; set; }

        string CreatedBy { get; set; }

        DateTime? ModifiedOn { get; set; }

        string ModifiedBy { get; set; }
    }
}
