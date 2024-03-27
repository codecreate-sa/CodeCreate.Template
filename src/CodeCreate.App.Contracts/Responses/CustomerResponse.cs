using System;

namespace CodeCreate.App.Contracts.Responses
{
    /// <summary>
    /// The customer details response
    /// </summary>
    public class CustomerResponse
    {
        /// <summary>
        /// The id of the customer
        /// </summary>
        public required Guid Id { get; init; }

        /// <summary>
        /// The customer's first name
        /// </summary>
        public required string FirstName { get; init; }

        /// <summary>
        /// The customer's last name
        /// </summary>
        public required string LastName { get; init; }

        /// <summary>
        /// The customer's email address
        /// </summary>
        public required string Email { get; init; }
    }
}