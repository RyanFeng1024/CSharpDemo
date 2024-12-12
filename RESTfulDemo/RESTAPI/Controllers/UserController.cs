using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTAPI.Models;

namespace RESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private static List<User> users = new() { 
            new User { Id = 1, Name = "John Doe", Email = "john@email.com" },
            new User { Id = 1, Name = "John Doe", Email = "john@email.com" }
        };

        [HttpGet]
        public IActionResult GetUser() 
        {
            return Ok(users);
        }


        [HttpPost]
        public IActionResult PostUser(User user)
        {
            if (user == null) return BadRequest("Invalid Data");
            user.Id = users.Count + 1;
            users.Add(user);
            return Ok(user);
        }
    }
}
