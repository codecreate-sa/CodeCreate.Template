namespace CodeCreate.App.Contracts.HealthChecks
{
    /// <summary>
    /// The health check details
    /// </summary>
    public class HealthCheck
    {
        /// <summary>
        /// The individual health check status
        /// </summary>
        public required string Status { get; init; }

        /// <summary>
        /// The individual health check component
        /// </summary>
        public required string Component { get; init; }

        /// <summary>
        /// The individual health check description
        /// </summary>
        public required string Description { get; init; }
    }
}