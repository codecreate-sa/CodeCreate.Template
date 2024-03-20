using CodeCreate.Domain.Models;
using CodeCreate.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeCreate.App.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
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
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet(Name = "GetCustomer")]
    public async Task<IEnumerable<CustomerDto>> GetAsync() =>
        await _customerService.GetAsync();
}
