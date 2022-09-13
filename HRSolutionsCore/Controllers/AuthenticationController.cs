using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRSolutionsCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost]
        [Route("redirectURI")]
        public IActionResult redirectURI(string redirect)
        {
            return Redirect(redirect);
        }
        [HttpPost]
        [Route("consentcallback")]
        public IActionResult consentCallback()
        {
            return Ok();
        }
    }
}
