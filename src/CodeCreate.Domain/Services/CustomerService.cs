using CodeCreate.Data.Contexts;
using CodeCreate.Data.Entities;
using CodeCreate.Domain.Models;
using CodeCreate.Domain.Options;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CodeCreate.Domain.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly TemplateDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public CustomerService(TemplateDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<(int totalCount, IEnumerable<CustomerDto>)> GetAllAsync(GetAllCustomersOptions getAllCustomersOptions, CancellationToken ct = default)
        {
            var customersQuery = _context.Customer.AsQueryable<Customer>();

            if (!string.IsNullOrEmpty(getAllCustomersOptions.SortField))
            {
                if (getAllCustomersOptions.SortOrder == SortOrder.Descending)
                {
                    customersQuery = customersQuery.OrderByDescending(GetSortProperty(getAllCustomersOptions));
                }
                else
                {
                    customersQuery = customersQuery.OrderBy(GetSortProperty(getAllCustomersOptions));
                }
            }

            var totalCount = await customersQuery.CountAsync(ct);

            customersQuery = customersQuery
                .Skip((getAllCustomersOptions.Page - 1) * getAllCustomersOptions.PageSize)
                .Take(getAllCustomersOptions.PageSize);

            var customers = await customersQuery
                .AsNoTracking()
                .Select(x => new CustomerDto
                {
                    Id = x.Id,
                    Email = x.Email,
                    FirstName = x.FirstName,
                    LastName = x.LastName
                }).ToListAsync(ct);

            return (totalCount, customers);
        }

        private static Expression<Func<Customer, object>> GetSortProperty(GetAllCustomersOptions getAllCustomersOptions) =>
            getAllCustomersOptions.SortField?.ToLower() switch
            {
                _ => customer => customer.Id,
            };
    }
}
