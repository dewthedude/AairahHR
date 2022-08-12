using HRSolutionsCore.BusinessLayer;
using HRSolutionsCore.Models;
using HRSolutionsCore.RequestModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HRSolutionsCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly TokenBusiness _tokenBussiness;

        public TokenController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            TokenBusiness tokenBusiness)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenBussiness = tokenBusiness;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] AdminLoginReqModel model)
        {
            var response =  _tokenBussiness.login(model); 
            if(response.Success)
            {
                return Ok(response.Data);
            }
            return Unauthorized();
        }
       
    }
}
