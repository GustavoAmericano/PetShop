using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PetShop.Core.ApplicationService;
using PetShop.Core.Entities;

namespace PetShop.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private IOwnerService _ownerService;

        public OwnersController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }
        // GET: api/Owner
        [HttpGet]
        public IEnumerable<Owner> Get()
        {
            return _ownerService.GetAllOwners();
        }

        // GET: api/Owner/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<Owner> Get(int id)
        {
            Owner owner = _ownerService.GetExtendedOwner(id);
            if(owner == null)
            {
                return BadRequest("Owner with Id " + id + " does not exist");
            }
            return owner;
        }

        // POST: api/Owner
        [HttpPost]
        public ActionResult<Owner> Post([FromBody] Owner owner)
        {
            if(owner.Id > 1)
            {
                return BadRequest("Cannot create owner with existing ID.");
            }
            return _ownerService.CreateOwner(owner);
        }

        // PUT: api/Owner/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Owner owner)
        {
            if(id != owner.Id)
            {
                return BadRequest("ID does not match owners id.");
            }
            _ownerService.SaveOwner(id, owner);
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _ownerService.DeleteOwner(id);
        }
    }
}
