using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetShop.Core.ApplicationService;
using PetShop.Core.Entities;

namespace PetShop.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly IPetService _petService;
        //private readonly IOwnerService _ownerService;

        public PetsController(IPetService petService, IOwnerService ownerService)
        {
            //_ownerService = ownerService;
            _petService = petService;
        }

        // GET api/values
        //[Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<Pet>> Get()
        {
            try
            {
                return _petService.GetAllPets().ToList();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/values/5
        //[Authorize]
        [HttpGet("{id}")]
        public ActionResult<Pet> Get(int id)
        {
            try
            {
                Pet pet = _petService.GetPetById(id);
                return pet;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/values
        //[Authorize(Roles = "ADMIN")]
        [HttpPost]
        public ActionResult<Pet> Post([FromBody] Pet pet)
        {
            try
            {
                return _petService.CreatePet(pet);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/values/5
        //[Authorize(Roles = "ADMIN")]
        [HttpPut("{id}")]
        public ActionResult<Pet> Put(int id, [FromBody] Pet pet)
        {
            try
            {
                return _petService.SavePet(id, pet);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/values/5
        //[Authorize(Roles = "ADMIN")]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _petService.DeletePet(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
