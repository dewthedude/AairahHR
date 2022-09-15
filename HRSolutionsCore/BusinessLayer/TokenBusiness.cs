using HRSolutions.BusinessLayer;
using HRSolutionsCore.Models;
using HRSolutionsCore.RequestModel;
using HRSolutionsCore.ResponseModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HRSolutionsCore.BusinessLayer
{
    public class TokenBusiness
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly HRManagementDbContext _context;
        public TokenBusiness(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            HRManagementDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;
        }
        public responseModel<addUpdateDeleteResponse, errorResponseModel> adminLogin(AdminLoginReqModel request)
        {
            bool isUserExist = _context.AdminRegistrations.Any(x => x.Email == request.UserName || x.Mobile == request.UserName);
            if (!isUserExist)
            {
                return new responseModel<addUpdateDeleteResponse, errorResponseModel> { errorResponse = new errorResponseModel { error = new errorModel { code = "", message = "Email or Mobile is not registered", Success = false } } };
            }
            var user = _context.AdminRegistrations.FirstOrDefault(x => x.Email == request.UserName && x.Password == request.Password || x.Mobile == request.UserName && x.Password == request.Password);

            if (user != null)
            {
                //var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                authClaims.Add(new Claim(ClaimTypes.Role, "Admin"));
                var token = GetToken(authClaims);
                return new responseModel<addUpdateDeleteResponse, errorResponseModel>
                {
                    successResponse = new addUpdateDeleteResponse
                    {
                        Message = "",
                        Data = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        },
                        Success = true
                    }
                };
            }
            return new responseModel<addUpdateDeleteResponse, errorResponseModel> { errorResponse = new errorResponseModel { error = new errorModel { code = "400", message = "Invalid Password", Success = false } } };
        }
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(60),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }
    }
}