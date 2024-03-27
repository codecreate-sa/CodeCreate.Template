using CodeCreate.Domain.Models;
using CodeCreate.Domain.Options;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CodeCreate.Domain.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<(int totalCount, IEnumerable<CustomerDto>)> GetAllAsync(GetAllCustomersOptions getAllCustomersOptions, CancellationToken ct = default);
    }
}