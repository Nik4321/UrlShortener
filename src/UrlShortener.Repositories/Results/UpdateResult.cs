using UrlShortener.Repositories.Enums;

namespace UrlShortener.Repositories.Results
{
    /// <summary>
    /// The result of a update entity operation
    /// </summary>
    public class UpdateResult<T>
    {
        /// <summary>
        /// Get a <see cref="UpdateResult{T}"/> with success status
        /// </summary>
        public static UpdateResult<T> Success => new UpdateResult<T>(UpdateResultStatus.Success);

        /// <summary>
        /// Get a <see cref="UpdateResult{T}"/> with not found status
        /// </summary>
        public static UpdateResult<T> NotFound => new UpdateResult<T>(UpdateResultStatus.NotFound);

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateResult{T}"/> class.
        /// </summary>
        /// <param name="status">The <see cref="UpdateResultStatus"/> of the result</param>
        public UpdateResult(UpdateResultStatus status)
        {
            this.Status = status;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateResult{T}"/> class. Sets the status property to success and returns the passed object.
        /// </summary>
        /// <param name="relatedObject">The related object.</param>
        public UpdateResult(T relatedObject)
        {
            this.Status = UpdateResultStatus.Success;
            this.RelatedObject = relatedObject;
        }

        /// <summary>
        /// The status of the update result
        /// </summary>
        public UpdateResultStatus Status { get; }

        /// <summary>
        /// Gets or sets the related object of the operation.
        /// </summary>
        public T RelatedObject { get; set; }
    }
}
