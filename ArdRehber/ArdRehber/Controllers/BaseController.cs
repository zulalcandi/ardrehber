using ArdRehber.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace ArdRehber.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected EUserType GetUserType()
        {
            var token = string.Empty;
            var header = (string)HttpContext.Request.Headers["Authorization"];
            if (header != null) { token = header.Substring(7); }

            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);

            int userTyp = int.Parse(jwtSecurityToken.Claims.First(claim => claim.Type == "typ").Value);

            return (EUserType)userTyp;
        }
    }
}
