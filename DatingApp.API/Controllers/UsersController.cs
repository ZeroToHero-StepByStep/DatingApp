using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDatatingRepository _repo;
       public UsersController(IDatatingRepository repo)
        {
            this._repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> getUsers()
        {
            var users = await _repo.GetUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getUser(int id)
        {
            var user = await _repo.GetUser(id);
            return Ok(user);
        }
    }
}
