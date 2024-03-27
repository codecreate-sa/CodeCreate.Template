using System.Collections.Generic;

namespace CodeCreate.App.Contracts.Responses
{
    /// <summary>
    /// The paged response class.
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public class PagedResponse<TResponse>
    {
        /// <summary>
        /// The items
        /// </summary>
        public required IEnumerable<TResponse> Items { get; init; } = [];

        /// <summary>
        /// The page size
        /// </summary>
        public required int PageSize { get; init; }

        /// <summary>
        /// The page number
        /// </summary>
        public required int Page { get; init; }

        /// <summary>
        /// The total amount of items
        /// </summary>
        public required int Total { get; init; }

        /// <summary>
        /// Whether the response has a next page or not
        /// </summary>
        public bool HasNextPage => Total > (Page * PageSize);
    }
}
