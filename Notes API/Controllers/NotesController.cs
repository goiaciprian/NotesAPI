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
    public class NotesController : ControllerBase
    {

        private static List<Note> _notes = new List<Note> {
            new Note { Id = Guid.NewGuid(), CategoryId = 1, OwnerId = new Guid("F7C707CC-BBDE-42D5-ABC0-8CD6FC6A09EF"), Title = "First Note", Description = "First Note Description" },
            new Note { Id = Guid.NewGuid(), CategoryId = 1, OwnerId = new Guid("F7C707CC-BBDE-42D5-ABC0-8CD6FC6A09EF"), Title = "Second Note", Description = "Second Note Description" },
            new Note { Id = Guid.NewGuid(), CategoryId = 2, OwnerId = new Guid("F7C707CC-BBDE-42D5-ABC0-8CD6FC6A09EF"), Title = "Third Note", Description = "Third Note Description" },
            new Note { Id = Guid.NewGuid(), CategoryId = 3, OwnerId = Guid.NewGuid(), Title = "Fourth Note", Description = "Fourth Note Description" },
            new Note { Id = Guid.NewGuid(), CategoryId = 3, OwnerId = Guid.NewGuid(), Title = "Fifth Note", Description = "Fifth Note Description" }
        };

        /// <summary>
        /// Returneaza toate notitele
        /// </summary>
        /// <returns>Lista de Notes</returns>
        /// <response code="200">Daca totul e ok</response>
        /// <response code="500">Daca este o problema pe server</response>
        [HttpGet]
        public IActionResult GetNotes()
        {
            return Ok(_notes);
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
            var note = _notes.First(n => n.Id == id);
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
            return Ok(_notes.Where(n => n.OwnerId == ownerId));
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
            note.Id = Guid.NewGuid();
            _notes.Add(note);
            return CreatedAtRoute("GetNoteById", new { id = note.Id }, note);
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
            var index = _notes.FindIndex(n => n.Id == id);
            if (index == -1)
                return NotFound();
                //return CreateNote(note);

            note.Id = id;
            _notes[index] = note;

            return Ok(note);
        }

        [HttpPut("{ownerId}/{noteId}")]
        public IActionResult UpdateNoteByNoteIdAndOwnerId([FromRoute] Guid noteId, [FromRoute] Guid ownerId, Note note)
        {
            var index = _notes.FindIndex(n => n.Id == noteId && n.OwnerId == ownerId);
            if (index == -1)
                return NotFound();
            note.Id = noteId;
            note.OwnerId = ownerId;
            _notes[index] = note;
            return Ok(note);

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
            var toDelete = _notes.FirstOrDefault(n => n.Id == id);
            if (toDelete == null)
                return NotFound();
            _notes.Remove(toDelete);
            return Ok(toDelete);
        }


        [HttpDelete("{ownerId}/{noteId}")]
        public IActionResult DeleteNoteByNoteIdAndOwnerId([FromRoute] Guid noteId, [FromRoute] Guid ownerId)
        {
            var note = _notes.FirstOrDefault(n => n.Id == noteId && n.OwnerId == ownerId);
            if (note == null)
                return NotFound();
            _notes.Remove(note);
            return Ok(note);
        }

        [HttpDelete("own/{ownerId}")]
        public IActionResult DeleteAllByOwnerId([FromRoute] Guid ownerId)
        {
            var notesList = _notes.Where(n => n.OwnerId == ownerId).ToList();

            if (notesList.Count == 0)
                return NotFound();

            _notes.RemoveAll(n => n.OwnerId == ownerId);
            return Ok(notesList);
        }
    }
}
