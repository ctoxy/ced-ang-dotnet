using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    //http:localhost:5000/api/auth
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;

        }
        // POST for register in api/auth
        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            //validate request

            username = username.ToLower();

            if (await _repo.UserExists(username))
                return BadRequest("Username already exist");
            
            var userToCreate = new User
            {
                Username = username
            };

            var createdUser = await _repo.Register(userToCreate, password);

            return StatusCode(201);            
        }
    }
}