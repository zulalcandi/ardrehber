#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArdRehber.Data;
using ArdRehber.Entities;
using ArdRehber.Dtos;
using System.Security.Claims;
using System.Web.Helpers;

namespace ArdRehber.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromForm] UserDto userDto)
        {
            //int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var kontrolUser = await _context.Users.AnyAsync(s => s.Name == userDto.Name && s.Surname == userDto.Surname && s.Email == userDto.Email && s.Password == userDto.Password);

            if (kontrolUser == true)
            {
                return BadRequest("Aynı kullanıcıyı tekrar ekleyemezsiniz.");
            }

            var user = new User()
            {
                Name = userDto.Name,
                Surname = userDto.Surname,
                Email = userDto.Email,
                Password = userDto.Password,
                RefreshToken = "ymMWEVXitFQWmOZmHlIOez6fEnFB5ROIIsasmSCPpT8=",
                RefreshTokenEndDate=new DateTime( 2022,5,29)

            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            //userDto.Id = user.Id;

            return Ok(user);
        }

        

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
