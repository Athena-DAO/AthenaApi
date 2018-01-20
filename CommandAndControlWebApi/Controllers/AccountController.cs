﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CommandAndControlWebApi.ViewModels;

namespace CommandAndControlWebApi.Controllers
{
    [Produces("application/json")]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;

        public AccountController(SignInManager<IdentityUser> signInManager, 
            UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.configuration = configuration;
        }

        [HttpPost]
        public async Task<object> Register([FromBody]RegistrationViewModel register)
        {
            var user = new IdentityUser
            {
                UserName = register.Email,
                Email = register.Email,
                PhoneNumber = register.PhoneNumber
            };

            var result = await userManager.CreateAsync(user, register.Password);
            if(result.Succeeded)
            {
                await signInManager.SignInAsync(user, false);
                return await GenerateJwtToken(register.Email, user);
            }

            throw new ApplicationException("Error");
        }

        private async Task<object> GenerateJwtToken(string email, IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtKey"]));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                configuration["JwtIssuer"],
                configuration["JetIssuer"],
                claims,
                expires: expires,
                signingCredentials: cred
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}