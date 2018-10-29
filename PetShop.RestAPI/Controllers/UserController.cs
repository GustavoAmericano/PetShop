using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetShop.Core.ApplicationService;
using PetShop.Core.DomainService;
using PetShop.Core.Entities;

namespace PetShop.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/User
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            try
            {
                return _userService.GetAllUsers().ToList();
            }
            catch (Exception)
            {
                return BadRequest("Failed to get users. Please try again.");
            }
        }

        // GET: api/User/5
        [Authorize(Roles = "ADMIN")]
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            try
            {
                return _userService.GetUserById(id);
            }
            catch (Exception)
            {
                return BadRequest($"Failed to get user with id {id}.");
            }
        }

        // POST: api/User
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public ActionResult<User> Post([FromBody] CreateUserInput user)
        {
            try
            {
                return _userService.CreateNewUser(user);
            }
            catch (Exception)
            {
                return BadRequest("Failed to create user.");
            }
        }

        // PUT: api/User/5
        [Authorize(Roles = "ADMIN")]
        [HttpPut("{id}")]
        public ActionResult<User> Put(int id, [FromBody] UpdateUserInput input)
        {
            try
            {
                if(id != input.Id) throw new ArgumentException();
                return _userService.UpdateUser(input);
            }
            catch (Exception e)
            {
                //return BadRequest("Failed to update user.");
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/ApiWithActions/5
        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{id}")]
        public ActionResult Delete([FromBody] User user)
        {
            try
            {
                _userService.DeleteUser(user);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest("Failed to delete user from database.");
            }
        }
    }
}
