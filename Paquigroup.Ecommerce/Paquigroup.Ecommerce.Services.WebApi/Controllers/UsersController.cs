using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Paquigroup.Ecommerce.Application.DTO;
using Paquigroup.Ecommerce.Application.Interface;
using Paquigroup.Ecommerce.Transversal.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Paquigroup.Ecommerce.Services.WebApi.Controllers
{

    [Route("api/[controller]/[action]")]
    [Consumes("application/json")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserApplication _userApplication;
        private readonly IConfiguration _configuration;
        public UsersController(IUserApplication userApplication, IConfiguration configuration)
        {
            _userApplication = userApplication;
            _configuration = configuration;

        }

        // POST api/<UsersController>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Authenticate([FromBody] UserDto usersDto)
        {

            var response = _userApplication.Authenticate(usersDto.Username, usersDto.Password);
            if (response.IsSuccess)
            {
                if (response.Data != null)
                {
                    response.Data.Token = BuildToken(response);
                    return Ok(response);

                }
                else
                    return NotFound(response.Message);

            }
            return BadRequest(response.Message);
        }

        private string BuildToken(Response<UserDto> usersDto)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Config:secret"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                  new Claim(ClaimTypes.Name, usersDto.Data.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration.GetValue<string>("Config:Issuer"),
                Audience = _configuration.GetValue<string>("Config:Audience")
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;

        }

    }
}
