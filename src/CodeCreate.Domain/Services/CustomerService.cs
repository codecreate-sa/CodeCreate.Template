using CodeCreate.Data.Contexts;
using CodeCreate.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeCreate.Domain.Services;

public class CustomerService : ICustomerService
{
    /// <summary>
    /// 
    /// </summary>
    private readonly TemplateDbContext _context;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public CustomerService(
        TemplateDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<CustomerDto>> GetAsync() =>
        await _context.Customer
            .Select(x => new CustomerDto()
            {
                Id = x.Id,
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName
            }).ToListAsync();
}
