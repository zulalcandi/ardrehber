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

namespace ArdRehber.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class LoginsController : ControllerBase
    //{
    //    private readonly DataContext _context;

    //    public LoginsController(DataContext context)
    //    {
    //        _context = context;
    //    }





        // POST: api/Logins
        // To protect from overposting attacks, see ttps://go.microsoft.com/fwlink/?linkid=2123754
    //    [HttpPost]
    //    public async Task<ActionResult<User>> PostLogin(LoginDto loginDto )
    //    {

    //         var user=new User()
    //        {
    //            Name = loginDto.UserName,
    //            Password = loginDto.Password,


    //        };


    //        _context.Users.Add(user);
    //        await _context.SaveChangesAsync();

    //        return CreatedAtAction("GetLogin", new { id = user.Id }, user);
    //    }


    //    private bool LoginExists(int id)
    //    {
    //        return _context.Users.Any(e => e.Id == id);
    //    }
    //}




    [ApiController]
    [Route("api/[controller]")]
    public class LoginsController : ControllerBase
    {
        readonly DataContext _context;
        readonly IConfiguration _configuration;
        public LoginsController(DataContext content, IConfiguration configuration)
        {
            _context = content;
            _configuration = configuration;
        }
        [HttpPost("[action]")]
        public async Task<bool> Create([FromForm] User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }
        [HttpPost("action")]
        public async Task<Entities.Token> Login([FromForm] LoginDto loginDto)
        {
            User user = await _context.Users.FirstOrDefaultAsync(x => x.Email == loginDto.UserName && x.Password == loginDto.Password);
            if (user != null)
            {
                //Token üretiliyor.
                TokenHandler tokenHandler = new TokenHandler(_configuration);
                Token token = tokenHandler.CreateAccessToken(user);

                //Refresh token Users tablosuna işleniyor.
                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenEndDate = token.Expiration.AddMinutes(3);
                await _context.SaveChangesAsync();

                return token;
            }
            return null;
        }
    }
}
