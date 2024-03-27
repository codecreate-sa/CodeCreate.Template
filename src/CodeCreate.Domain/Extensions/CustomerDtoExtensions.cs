using CodeCreate.App.Contracts.Responses;
using CodeCreate.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace CodeCreate.Domain.Extensions
{
    public static class CustomerDtoExtensions
    {
        public static CustomerResponse ToCustomerResponse(this CustomerDto customerDto)
        {
            return new CustomerResponse
            {
                Id = customerDto.Id,
                Email = customerDto.Email,
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName
            };
        }

        public static CustomersResponse ToCustomersResponse(this IEnumerable<CustomerDto> customerDtos, int page, int pageSize, int totalCount)
        {
            return new CustomersResponse
            {
                Items = customerDtos.Select(ToCustomerResponse),
                Page = page,
                PageSize = pageSize,
                Total = totalCount
            };
        }
    }
}
