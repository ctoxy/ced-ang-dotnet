using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
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
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            if (userForRegisterDto is null)
            {
                throw new System.ArgumentNullException(nameof(userForRegisterDto));
            }
            //validate request

            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

            if (await _repo.UserExists(userForRegisterDto.Username))
                return BadRequest("Username already exist");
            
            var userToCreate = new User
            {
                Username = userForRegisterDto.Username
            };

            var createdUser = await _repo.Register(userToCreate, userForRegisterDto.Password);

            return StatusCode(201);            
        }
    }
}