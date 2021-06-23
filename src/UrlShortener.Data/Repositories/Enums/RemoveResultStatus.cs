namespace UrlShortener.Data.Repositories.Enums
{
    /// <summary>
    /// Remove result status
    /// </summary>
    public enum RemoveResultStatus
    {
        /// <summary>
        /// The entity was successfully removed
        /// </summary>
        Success,

        /// <summary>
        /// The entity could not be found
        /// </summary>
        NotFound,

        /// <summary>
        /// The entity has protected dependencies and could not be removed
        /// </summary>
        ProtectedDependencies
    }
}
