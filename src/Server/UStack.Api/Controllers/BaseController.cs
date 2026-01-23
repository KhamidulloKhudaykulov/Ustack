using Microsoft.AspNetCore.Mvc;
using UStack.Identity.Domain.Outcome;

namespace UStack.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected IActionResult Problem(Error error)
        {
            return Problem(
                title: error.Code,
                detail: error.Message,
                statusCode: StatusCodes.Status400BadRequest
            );
        }

    }
}
