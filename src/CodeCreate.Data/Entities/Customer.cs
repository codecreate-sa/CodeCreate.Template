using System;

namespace CodeCreate.Data.Entities;

public class Customer
{
    /// <summary>
    /// 
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    public string Email { get; set; } = string.Empty;
}
