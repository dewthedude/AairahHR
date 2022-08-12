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

        public TokenBusiness(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public AddUpdateDeleteResponse login(AdminLoginReqModel request)
        {
            var user = new
            {
                username = "AshuKhan",
                password = "12345",
                Role = "Admin",

            };
            //var user = await _userManager.FindByNameAsync(model.Username);

            //if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            if (user != null)
            {
                //var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                authClaims.Add(new Claim(ClaimTypes.Role, "Admin"));


                var token =  GetToken(authClaims);

                return new AddUpdateDeleteResponse
                {
                    Message = "",
                    Data = new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    },
                    Success = true
                };

            }
            return new AddUpdateDeleteResponse { Data = "", Message = "", Success = false };
        }
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
