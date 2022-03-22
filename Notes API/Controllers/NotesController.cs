using Microsoft.AspNetCore.Mvc;
using Notes_API.Models;
using Notes_API.Services;
using System;

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
        public IActionResult GetNotes()
        {
            return Ok(_noteService.GetAll());
        }

        /// <summary>
        /// Returneaza 1 notita dupa Id
        /// </summary>
        /// <returns>Note</returns>
        /// <response code="200">Daca totul e ok</response>
        /// <response code="404">Daca nu se gaseste resursa</response>
        /// <response code="500">Daca este o problema pe server</response>
        [HttpGet("{id}", Name = "GetNoteById")]
        public IActionResult GetNoteById([FromRoute] Guid id)
        {
            var note = _noteService.Get(id);
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
        public IActionResult GetNotesByOwnerId([FromRoute] Guid ownerId)
        {
            return Ok(_noteService.GetNotesByOwnerId(ownerId));
        }

        /// <summary>
        /// Adauga o notita noua
        /// </summary>
        /// <returns>Note</returns>
        /// <response code="200">Daca totul e ok</response>
        /// <response code="500">Daca este o problema pe server</response>
        [HttpPost]
        public IActionResult CreateNote([FromBody] Note note)
        {
            return CreatedAtRoute("GetNoteById", new { id = note.Id }, _noteService.Create(note));
        }


        /// <summary>
        /// Actualizaza o notita
        /// </summary>
        /// <returns>Note</returns>
        /// <response code="200">Daca totul e ok</response>
        /// <response code="404">Daca nu se gaseste resursa</response>
        /// <response code="500">Daca este o problema pe server</response>
        [HttpPut]
        public IActionResult UpdateNote([FromRoute] Guid id, [FromBody] Note note)
        {
            return Ok(_noteService.Update(id, note));
        }

        [HttpPut("{ownerId}/{noteId}")]
        public IActionResult UpdateNoteByNoteIdAndOwnerId([FromRoute] Guid noteId, [FromRoute] Guid ownerId, Note note)
        {
            return Ok(_noteService.UpdateNoteByNoteIdAndOwnerId(noteId, ownerId, note));
        }

        /// <summary>
        /// Sterge o notita
        /// </summary>
        /// <returns>Note</returns>
        /// <response code="200">Daca totul e ok</response>
        /// <response code="404">Daca nu se gaseste resursa</response>
        /// <response code="500">Daca este o problema pe server</response>
        [HttpDelete("{id}")]
        public IActionResult DeleteNote([FromRoute] Guid id)
        {
            var toDelete = _noteService.Delete(id);
            if (toDelete == null)
                return NotFound();
            return Ok(toDelete);
        }


        [HttpDelete("{ownerId}/{noteId}")]
        public IActionResult DeleteNoteByNoteIdAndOwnerId([FromRoute] Guid noteId, [FromRoute] Guid ownerId)
        {
            var note = _noteService.DeleteNoteByNoteIdAndOwnerId(noteId, ownerId);
            if (note == null)
                return NotFound();
            return Ok(note);
        }

        [HttpDelete("own/{ownerId}")]
        public IActionResult DeleteAllByOwnerId([FromRoute] Guid ownerId)
        {
            var notesList = _noteService.DeleteAllByOwnerId(ownerId);

            if (notesList.Count == 0)
                return NotFound();

            return Ok(notesList);
        }
    }
}
