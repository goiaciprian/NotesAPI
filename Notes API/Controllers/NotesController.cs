using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Notes_API.Models;
using Notes_API.Services;
using System;
using System.Threading.Tasks;

namespace Notes_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private INoteCollectionService _noteService;

        public NotesController(INoteCollectionService noteService)
        {
            _noteService = noteService ?? throw new ArgumentNullException(nameof(noteService));
        }

        /// <summary>
        /// Returneaza toate notitele
        /// </summary>
        /// <returns>Lista de Notes</returns>
        /// <response code="200">Daca totul e ok</response>
        /// <response code="500">Daca este o problema pe server</response>
        [HttpGet]
        public async Task<IActionResult> GetNotes()
        {
            return Ok(await _noteService.GetAll());
        }

        /// <summary>
        /// Returneaza 1 notita dupa Id
        /// </summary>
        /// <returns>Note</returns>
        /// <response code="200">Daca totul e ok</response>
        /// <response code="404">Daca nu se gaseste resursa</response>
        /// <response code="500">Daca este o problema pe server</response>
        [HttpGet("{id}", Name = "GetNoteById")]
        public async Task<IActionResult> GetNoteById([FromRoute] Guid id)
        {
            var note = await _noteService.Get(id);
            if (note == null)
                return NotFound();
            return Ok(note);
        }


        /// <summary>
        /// Returneaza notite dupa owner id 
        /// </summary>
        /// <returns>Notes List</returns>
        /// <response code="200">Daca totul e ok</response>
        /// <response code="500">Daca este o problema pe server</response>
        [HttpGet("own/{ownerId}")]
        public async Task<IActionResult> GetNotesByOwnerId([FromRoute] Guid ownerId)
        {
            return Ok(await _noteService.GetNotesByOwnerId(ownerId));
        }

        /// <summary>
        /// Adauga o notita noua
        /// </summary>
        /// <returns>Note</returns>
        /// <response code="200">Daca totul e ok</response>
        /// <response code="500">Daca este o problema pe server</response>
        [HttpPost]
        public async Task<IActionResult> CreateNote([FromBody] Note note)
        {
            var newNote = await _noteService.Create(note);
            return CreatedAtRoute("GetNoteById", new { id = newNote.Id }, newNote);
        }


        /// <summary>
        /// Actualizaza o notita
        /// </summary>
        /// <returns>Note</returns>
        /// <response code="200">Daca totul e ok</response>
        /// <response code="404">Daca nu se gaseste resursa</response>
        /// <response code="500">Daca este o problema pe server</response>
        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateNote([FromRoute] Guid id, [FromBody] Note note)
        {
            note.Id = id;
            return Ok(await _noteService.Update(id, note));
        }

        [HttpPut("own/{ownerId}/{noteId}")]
        public async Task<IActionResult> UpdateNoteByNoteIdAndOwnerId([FromRoute] Guid noteId, [FromRoute] Guid ownerId, Note note)
        {
            return Ok(await _noteService.UpdateNoteByNoteIdAndOwnerId(noteId, ownerId, note));
        }

        /// <summary>
        /// Sterge o notita
        /// </summary>
        /// <returns>Note</returns>
        /// <response code="200">Daca totul e ok</response>
        /// <response code="404">Daca nu se gaseste resursa</response>
        /// <response code="500">Daca este o problema pe server</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote([FromRoute] Guid id)
        {
            var toDelete = await _noteService.Delete(id);
            if (toDelete == null)
                return NotFound();
            return Ok(toDelete);
        }


        [HttpDelete("own/{ownerId}/{noteId}")]
        public async Task<IActionResult> DeleteNoteByNoteIdAndOwnerId([FromRoute] Guid noteId, [FromRoute] Guid ownerId)
        {
            var note = await _noteService.DeleteNoteByNoteIdAndOwnerId(noteId, ownerId);
            if (note == null)
                return NotFound();
            return Ok(note);
        }

        [HttpDelete("own/{ownerId}")]
        public async Task<IActionResult> DeleteAllByOwnerId([FromRoute] Guid ownerId)
        {
            return Ok(await _noteService.DeleteAllByOwnerId(ownerId));
        }
    }
}
