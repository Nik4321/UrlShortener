using System;
using System.Collections.Generic;
using UrlShortener.Repositories.Enums;

namespace UrlShortener.Repositories.Results
{
    /// <summary>
    /// The result of a remove entity operation
    /// </summary>
    public class RemoveResult
    {
        /// <summary>
        /// Get a <see cref="RemoveResult"/> with success status
        /// </summary>
        public static RemoveResult Success => new RemoveResult(RemoveResultStatus.Success);

        /// <summary>
        /// Get a <see cref="RemoveResult"/> with not found status
        /// </summary>
        public static RemoveResult NotFound => new RemoveResult(RemoveResultStatus.NotFound);

        /// <summary>
        /// The status of the remove result
        /// </summary>
        public RemoveResultStatus Status { get; }

        /// <summary>
        /// The dependencies if the status is ProtectedDependency
        /// </summary>
        public IList<Type> Dependencies { get; }

        private RemoveResult(RemoveResultStatus status)
        {
            this.Status = status;
            this.Dependencies = new List<Type>();
        }
    }
}
