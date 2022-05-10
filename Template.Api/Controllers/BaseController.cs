using Microsoft.AspNetCore.Mvc;

namespace Template.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        [NonAction]
        public new IActionResult Response(object result)
        {
            return Ok(new
            {
                success = true,
                data = result
            });
        }
    }
}