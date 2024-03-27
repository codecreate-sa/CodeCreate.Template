using Asp.Versioning;
using CodeCreate.App.Contracts.Requests;
using CodeCreate.App.Contracts.Responses;
using CodeCreate.Domain.Extensions;
using CodeCreate.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace CodeCreate.App.Controllers
{
    /// <summary>
    /// The controller responsible for the customers resource
    /// </summary>
    [ApiVersion(1.0)]
    public class CustomerController : ApiControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ICustomerService _customerService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerService"></param>
        public CustomerController(
            ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Returns the details of all customers.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>An ActionResult of CustomersResponse</returns>
        /// <response code="200">Returns all customers</response>
        /// <response code="500">Returns 500 if error has occured on the API side</response>
        [ProducesResponseType(typeof(CustomersResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet(ApiEndpoints.Customers.GetAll)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllCustomersRequest request, CancellationToken cancellationToken)
        {
            (int customersCount, var customers) = 
                await _customerService.GetAllAsync(request.ToGetAllCustomersOptions(), cancellationToken);

            return Ok(customers.ToCustomersResponse(request.Page, request.PageSize, customersCount));
        }
    }
}
