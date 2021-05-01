using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task.Models;

namespace Task.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IRepository<User> userRepository;

        public IHostingEnvironment Hosting { get; }

        public UserController(IRepository<User> UserRepository, IHostingEnvironment hosting)
        {
            userRepository = UserRepository;
            Hosting = hosting;
        }

        [HttpGet]
        public IActionResult GetUser()
        {
            IList<User> UserList = userRepository.Get();
            return Ok(UserList);
        }

        [HttpDelete]
        public IActionResult DeleteUser(int id)
        {
            userRepository.Delete(id);
            return Ok("Delete Successfully");
        }

        [HttpGet]
        public IActionResult GetUserById(int id)
        {
            User userInDb = userRepository.Find(id);
            return Ok(userInDb);
        }

        [HttpPost]
        public IActionResult CreateNewUser(User NewUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            userRepository.Add(NewUser);
            return Ok(NewUser);
        }

        [HttpPost]
        public IActionResult EditUser(int id , User user)
        {
            userRepository.Update(id,user);
            return Ok();
        }
    }
}
