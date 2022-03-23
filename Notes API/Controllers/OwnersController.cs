using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Notes_API.Models;
using Notes_API.Services.OwnerService;
using System;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetOwners()
        {
            return Ok(await _ownerService.GetAll());
        }

        /// <summary>
        /// Returneaza 1 owner
        /// </summary>
        /// <returns>Owner</returns>
        /// <response code="200">Daca totul e ok</response>
        /// <response code="404">Daca nu se gaseste resursa</response>
        /// <response code="500">Daca este o problema pe server</response>
        [HttpGet("{id}", Name = "GetOwnerById")]
        public async Task<IActionResult> GetOwnerById([FromRoute] Guid id)
        {
            var owner = await _ownerService.Get(id);
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
        public async Task<IActionResult> CreateOwner([FromBody] Owner owner)
        {
            var newOwner = await _ownerService.Create(owner);
            return CreatedAtRoute("GetOwnerById", new { id = newOwner.Id }, newOwner);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateOwner([FromRoute] Guid id, [FromBody] Owner owner)
        {
            owner.Id = id;
            var ownerFoundIndex = await _ownerService.Update(id, owner);
            if(ownerFoundIndex == null)
            {
                return NotFound();
            }
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
        public async Task<IActionResult> DeleteOwner([FromRoute] Guid id)
        {
            var toDelete = await _ownerService.Delete(id);
            if (toDelete == null)
                return NotFound();
            return Ok(toDelete);
        }
    }
}
