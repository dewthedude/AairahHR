using HRSolutionsCore.BusinessLayer;
using HRSolutionsCore.Models;
using HRSolutionsCore.RequestModel;
using HRSolutionsCore.ResponseModel;
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
        [Route("adminLogin")]
        public async Task<IActionResult> adminLogin([FromBody] AdminLoginReqModel model)
        {
            //Validating request body
            var validator = new AdminLoginReqModelValidator();
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                return BadRequest(new responseModel<addUpdateDeleteResponse, errorResponseModel> { errorResponse = new errorResponseModel { error = new errorModel { message = validationResult.Errors[0].ErrorMessage } } });
            }
            var response = _tokenBussiness.adminLogin(model);
            if (response.successResponse != null)
            {
                return Ok(response.successResponse);
            }
            if (response.errorResponse.error.message.Contains("not registered"))
            {
                return BadRequest(response.errorResponse);
            }
            if (response.errorResponse.error.message.Contains("Invalid"))
            {
                return BadRequest(response.errorResponse);
            }
            return Unauthorized();
        }
    }
}
