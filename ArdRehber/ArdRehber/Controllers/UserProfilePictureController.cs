using ArdRehber.Dtos;
using ArdRehber.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArdRehber.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfilePictureController : BaseController
    {
        //public async Task<IActionResult> Create([FromBody] ImageDto imageDto)
        //{
        //    byte[] base64EncodedBytes = Convert.FromBase64String(imageDto.ImageBase64);
        //    byte[] image;
        //    _context.Users.Add(image);
        //    await _context.SaveChangesAsync();

        //}
    }
}
