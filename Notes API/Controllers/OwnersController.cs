using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notes_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Notes_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private static List<Owner> _owners = new List<Owner>()
        {
            new Owner() { Id = new Guid("F7C707CC-BBDE-42D5-ABC0-8CD6FC6A09EF"), Name = "Owner 1"},
            new Owner() { Id = Guid.NewGuid(), Name = "Owner 2"},
            new Owner() { Id = Guid.NewGuid(), Name = "Owner 3"}
        };

        /// <summary>
        /// Retirneaza toti ownerii
        /// </summary>
        /// <returns>Owner list</returns>
        /// <response code="200">Daca totul e ok</response>
        /// <response code="500">Daca este o problema pe server</response>
        [HttpGet]
        public IActionResult GetOwners()
        {
            return Ok(_owners);
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
            var owner = _owners.FirstOrDefault(o => o.Id == id);
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
            owner.Id = Guid.NewGuid();
            _owners.Add(owner);
            return CreatedAtRoute("GetOwnerById", new { id = owner.Id }, owner);
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
            var toDelete = _owners.First(o => o.Id == id);
            if (toDelete == null)
                return NotFound();
            _owners.Remove(toDelete);
            return Ok(toDelete);
        }
    }
}
