namespace CodeCreate.App
{
    /// <summary>
    /// The constants for the routes of the API endpoints
    /// </summary>
    public static class ApiEndpoints
    {
        private const string ApiBase = "api";

        /// <summary>
        /// The route constants for the Customers endpoints
        /// </summary>
        public static class Customers
        {
            private const string Base = $"{ApiBase}/customers";

            /// <summary>
            /// Create endpoint
            /// </summary>
            public const string Create = Base;

            /// <summary>
            /// Get by id endpoint
            /// </summary>
            public const string Get = $"{Base}/{{id:guid}}";

            /// <summary>
            /// Get all endpoint
            /// </summary>
            public const string GetAll = Base;
        }
    }
}
