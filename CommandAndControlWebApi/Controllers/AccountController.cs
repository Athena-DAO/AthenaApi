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
using CommandAndControlWebApi.DAL;

namespace CommandAndControlWebApi.Controllers
{
    [Produces("application/json")]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly DataCenterContext dataCenterContext;

        public AccountController(SignInManager<IdentityUser> signInManager, 
            UserManager<IdentityUser> userManager, IConfiguration configuration, 
            DataCenterContext dataCenterContext)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.configuration = configuration;
            this.dataCenterContext = dataCenterContext;
        }

        [HttpPost]
        public async Task<object> Login([FromBody]LoginViewModel login)
        {
            var result = await signInManager.PasswordSignInAsync(login.UserName, login.Password, false, false);

            if(result.Succeeded)
            {
                var appUser = userManager.Users.SingleOrDefault(x => x.Email == login.UserName);
                var role = await userManager.GetRolesAsync(appUser);
                return Ok(new
                {
                    token = GenerateJwtToken(login.UserName, appUser, role[0])
                }); 
            }

            return BadRequest("User name or password incorrect");
        }

        [HttpPost]
        public async Task<object> Register([FromBody]RegistrationViewModel register)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = register.Email,
                    Email = register.Email
                };

                var result = await userManager.CreateAsync(user, register.Password);
                if (result.Succeeded)
                {
                    user = await userManager.FindByEmailAsync(register.Email);
                    dataCenterContext.Profiles.Add(new Models.Profile
                    {
                        FirstName = register.FirstName,
                        LastName = register.LastName,
                        Email = register.Email,
                        Id = Guid.Parse(user.Id),
                        CoverPicture = "URL",
                        ProfilePicture = "URL"
                    });
                    dataCenterContext.SaveChanges();
                    await userManager.AddToRoleAsync(user, "User");
                    return Ok();
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        public async Task<IActionResult> IsEmailAvailable([FromBody]IsEmailAvailableViewModel viewModel)
        {
            IdentityUser user = await userManager.FindByEmailAsync(viewModel.Email);

            if(user == null)
            {
                return Ok(new
                {
                    EmailAvailable = true
                });
            }
            return Ok(new
            {
                EmailAvailable = false
            });
        }

        [HttpPost]
        public async Task<IActionResult> LogoutAsync()
        {
            await signInManager.SignOutAsync();
            return Ok();
        }

        private string GenerateJwtToken(string email, IdentityUser user, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtKey"]));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JwtIssuer"],
                audience: configuration["JwtAudience"],
                claims: claims,
                expires: expires,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}