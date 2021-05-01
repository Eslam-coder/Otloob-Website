using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Task.Models;

namespace Task.Controllers
{
    [ApiController]
    [Route("api/[controller]/[Action]")]
    public class AccountController : Controller
    {
        private readonly IRepository<User> userRepository;
        public AccountController(IRepository<User> UserRepository)
        {
            userRepository = UserRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        public async System.Threading.Tasks.Task<IActionResult> RegisterAsync(User newUser)
        {
            if (ModelState.IsValid)
            {
                //Hashing password
                var HashedPassword = HashPassword.HashhPassword(newUser.Password);
                var userNew = new User
                {
                    Name = newUser.Name,
                    PhoneNumber = newUser.PhoneNumber,
                    Email = newUser.Email,
                    Password = HashedPassword
                };
                userRepository.Add(userNew);
                var claims = new List<Claim>
                {
                  new Claim(ClaimTypes.Email, newUser.Email)
                };

                var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    //RedirectUri = "/Home/Index",
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                return Ok(new { UserEamil = userNew.Email, UserId = userNew.UserID });
            }
            return BadRequest();
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<IActionResult> LoginAsync(User user)
        {
            //Hashing password
            var HashedPassword = HashPassword.HashhPassword(user.Password);
            var userToLogin = new User
            {
                Email = user.Email,
                Password = HashedPassword
            };
            User userInDb = userRepository.Login(userToLogin);
            if (userInDb != null)
            {
                var claims = new List<Claim>
                {
                  new Claim(ClaimTypes.Email, user.Email),
                  new Claim("Password", user.Password)
                };

                var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    //RedirectUri = "/Home/Index",
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                return Ok(new { UserEamil = userInDb.Email, UserId = userInDb.UserID });
            }
            return BadRequest();
        }


        [HttpPost]
        public async System.Threading.Tasks.Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }

    }
}
