﻿using System;

namespace UrlShortener.Data.Models.Interfaces
{
    public interface IAudit
    {
        DateTime CreatedOn { get; set; }

        DateTime? ModifiedOn { get; set; }
    }
}
