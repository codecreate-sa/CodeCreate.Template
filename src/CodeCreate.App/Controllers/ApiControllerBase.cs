using Microsoft.AspNetCore.Mvc;

namespace CodeCreate.App.Controllers
{
    /// <summary>
    /// ApiControllerBase is an abstract base class that all the controllers of our API
    /// will inherit from. This is done in order for every controller to have access 
    /// to common properties and applied attributes and not having to put in every 
    /// single controller we create.
    /// </summary>
    [Produces("application/json")]
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase
    {
    }
}
