using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PetShop.Core.ApplicationService;
using PetShop.Core.DomainService;
using PetShop.Core.Entities;
using PetShop.RestApi.Helpers;

namespace PetShop.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IUserService _userService;

        public TokenController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public IActionResult Login([FromBody]LoginInput input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            if (input.Username == null || input.Password == null) return BadRequest("NO INPUT");
            try
            {
                User user = _userService.ValidateUser(input);
                return Ok(new { username = user.Username, token = GenerateToken(user) });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception)
            {
                return BadRequest("An error occured. Please try again.");
            }
            
        }


        private string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
            };

            if (user.IsAdmin)
                claims.Add(new Claim(ClaimTypes.Role, "ADMIN"));

            var token = new JwtSecurityToken(
                new JwtHeader(
                    new SigningCredentials(
                        JwtSecurityKey.Key,
                        SecurityAlgorithms.HmacSha256)),
                new JwtPayload(null,
                    null,
                    claims.ToArray(),
                    DateTime.Now,
                    DateTime.Now.AddDays(1)));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
