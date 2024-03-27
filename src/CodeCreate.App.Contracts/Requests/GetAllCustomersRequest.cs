namespace CodeCreate.App.Contracts.Requests
{
    /// <summary>
    /// The query parameters for the get all customers endpoint
    /// </summary>
    public class GetAllCustomersRequest : PagedRequest
    {
        /// <summary>
        /// The sort by element
        /// </summary>
        public required string? SortBy { get; init; }
    }
}
