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
    public class ColorsController : ControllerBase
    {
        private readonly IColorService colorService;

        public ColorsController(IColorService colorService)
        {
            this.colorService = colorService;
        }
        
        // GET
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<Color>> Get()
        {
            return colorService.GetAllColors().ToList();
        }


        // GET api/values/5
        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<Color> Get(int id)
        {
            return colorService.GetColorById(id);
        }

        // POST api/values
        [Authorize (Roles = "ADMIN")]
        [HttpPost]
        public ActionResult Post([FromBody] Color color)
        {
            try
            {
                colorService.CreateColor(color);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }


        // PUT api/values/5
        [Authorize(Roles = "ADMIN")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Color color)
        {
            colorService.UpdateColor(color);
        }

        // DELETE api/values/5
        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            colorService.DeleteColor(id);
        }
    }
}