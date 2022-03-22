using Microsoft.AspNetCore.Mvc;
using Notes_API.Models;
using Notes_API.Services.OwnerService;
using System;

namespace Notes_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private IOwnerService _ownerService;

        public OwnersController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }
        

        /// <summary>
        /// Retirneaza toti ownerii
        /// </summary>
        /// <returns>Owner list</returns>
        /// <response code="200">Daca totul e ok</response>
        /// <response code="500">Daca este o problema pe server</response>
        [HttpGet]
        public IActionResult GetOwners()
        {
            return Ok(_ownerService.GetAll());
        }

        /// <summary>
        /// Returneaza 1 owner
        /// </summary>
        /// <returns>Owner</returns>
        /// <response code="200">Daca totul e ok</response>
        /// <response code="404">Daca nu se gaseste resursa</response>
        /// <response code="500">Daca este o problema pe server</response>
        [HttpGet("{id}", Name = "GetOwnerById")]
        public IActionResult GetOwnerById([FromRoute] Guid id)
        {
            var owner = _ownerService.Get(id);
            if (owner == null)
                return NotFound();
            return Ok(owner);
        }


        /// <summary>
        /// Adauga owner
        /// </summary>
        /// <returns>Owner</returns>
        /// <response code="200">Daca totul e ok</response>
        /// <response code="500">Daca este o problema pe server</response>
        [HttpPost]
        public IActionResult CreateOwner([FromBody] Owner owner)
        {
            return CreatedAtRoute("GetOwnerById", new { id = owner.Id }, _ownerService.Create(owner));
        }

        [HttpPost("{id}")]
        public IActionResult UpdateOwner([FromRoute] Guid id, [FromBody] Owner owner)
        {
            var ownerFoundIndex = _ownerService.Update(id, owner);
            if(ownerFoundIndex == null)
            {
                return NotFound();
            }
            owner.Id = id;
            return Ok(owner);

        }

        /// <summary>
        /// Sterge 1 owner
        /// </summary>
        /// <returns>Owner</returns>
        /// <response code="200">Daca totul e ok</response>
        /// <response code="404">Daca nu se gaseste resursa</response>
        /// <response code="500">Daca este o problema pe server</response>
        [HttpDelete("{id}")]
        public IActionResult DeleteOwner([FromRoute] Guid id)
        {
            var toDelete = _ownerService.Delete(id);
            if (toDelete == null)
                return NotFound();
            return Ok(toDelete);
        }
    }
}
