namespace CodeCreate.Domain.Options
{
    /// <summary>
    /// The GetAllCustomersOptions class containing the information for sorting and filtering the customers.
    /// </summary>
    public record GetAllCustomersOptions
    {
        /// <summary>
        /// The sort field
        /// </summary>
        public string? SortField { get; init; }

        /// <summary>
        /// The sort order
        /// </summary>
        public SortOrder? SortOrder { get; init; }

        /// <summary>
        /// The page number
        /// </summary>
        public int Page { get; init; }

        /// <summary>
        /// The page size
        /// </summary>
        public int PageSize { get; init; }
    }

    /// <summary>
    /// The SortOrder enum
    /// </summary>
    public enum SortOrder
    {
        Unsorted,
        Ascending,
        Descending
    }
}
