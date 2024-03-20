using CodeCreate.Domain.Models;

namespace CodeCreate.Domain.Services;

/// <summary>
/// 
/// </summary>
public interface ICustomerService
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<CustomerDto>> GetAsync();
}