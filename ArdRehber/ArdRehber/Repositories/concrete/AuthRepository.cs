﻿using ArdRehber.Data;
using ArdRehber.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArdRehber.Repositories.concrete
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<User> Login(string username, string pasword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Name == username); //Get user from database.
            if (user == null)
                return null; // User does not exist.

            //if (!VerifyPassword(user.Password, user.PasswordHash, user.PasswordSalt))
            //    return null;

            return user;
        }



        //private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        //{
        //    using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
        //    {
        //        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)); // Create hash using password salt.
        //        for (int i = 0; i < computedHash.Length; i++)
        //        { // Loop through the byte array
        //            if (computedHash[i] != passwordHash[i]) return false; // if mismatch
        //        }
        //    }
        //    return true; //if no mismatches.
        //}

        //public async Task<User> Register(User user, string password)
        //{
        //    byte[] passwordHash, passwordSalt;
        //    CreatePasswordHash(password, out passwordHash, out passwordSalt);

        //    user.PasswordHash = passwordHash;
        //    user.PasswordSalt = passwordSalt;

        //    await _context.Users.AddAsync(user); // Adding the user to context of users.
        //    await _context.SaveChangesAsync(); // Save changes to database.

        //    return user;
        //}
        //private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        //{
        //    using (var hmac = new System.Security.Cryptography.HMACSHA512())
        //    {
        //        passwordSalt = hmac.Key;
        //        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //    }
        //}
        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.Name == username))
                return true;
            return false;
        }

        public Task<User> Register(User user, string password)
        {
            throw new NotImplementedException();
        }
    }
}
