using System;
using System.Collections.Generic;

namespace CodeCreate.App.Contracts.HealthChecks
{
    /// <summary>
    /// The health check response details
    /// </summary>
    public class HealthCheckResponse
    {
        /// <summary>
        /// The total health check status
        /// </summary>
        public required string Status { get; init; }

        /// <summary>
        /// The list of health checks made
        /// </summary>
        public required IEnumerable<HealthCheck> Checks { get; init; } = [];

        /// <summary>
        /// The total duration of the health checks made
        /// </summary>
        public required TimeSpan Duration { get; init; }
    }
}