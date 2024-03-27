namespace CodeCreate.App.Contracts.Requests
{
    /// <summary>
    /// The paged request parameters
    /// </summary>
    public class PagedRequest
    {
        /// <summary>
        /// The page number. Defaults to 1
        /// </summary>
        public required int Page { get; init; } = 1;

        /// <summary>
        /// The page size. Defaults to 10
        /// </summary>
        public required int PageSize { get; init; } = 10;
    }
}
