using Notes_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Notes_API.Services
{
    public class NoteCollectionService : INoteCollectionService
    {

        private static List<Note> _notes = new List<Note> {
            new Note { Id = Guid.NewGuid(), CategoryId = 1, OwnerId = new Guid("F7C707CC-BBDE-42D5-ABC0-8CD6FC6A09EF"), Title = "First Note", Description = "First Note Description" },
            new Note { Id = Guid.NewGuid(), CategoryId = 1, OwnerId = new Guid("F7C707CC-BBDE-42D5-ABC0-8CD6FC6A09EF"), Title = "Second Note", Description = "Second Note Description" },
            new Note { Id = Guid.NewGuid(), CategoryId = 2, OwnerId = new Guid("F7C707CC-BBDE-42D5-ABC0-8CD6FC6A09EF"), Title = "Third Note", Description = "Third Note Description" },
            new Note { Id = Guid.NewGuid(), CategoryId = 3, OwnerId = Guid.NewGuid(), Title = "Fourth Note", Description = "Fourth Note Description" },
            new Note { Id = Guid.NewGuid(), CategoryId = 3, OwnerId = Guid.NewGuid(), Title = "Fifth Note", Description = "Fifth Note Description" }
        };

        public NoteCollectionService()
        {

        }


        public Note Create(Note model)
        {
            model.Id = Guid.NewGuid();
            _notes.Add(model);
            return model;
        }

        public Note Delete(Note model)
        {
            throw new NotImplementedException();
        }

        public Note Delete(Guid Id)
        {
            var note = _notes.FirstOrDefault(x => x.Id == Id);
            if (note == null)
                return null;
            _notes.Remove(note);
            return note;
        }

        public List<Note> DeleteAllByOwnerId(Guid ownerId)
        {
            var notes = _notes.Where(x => x.OwnerId == ownerId).ToList();
            _notes.RemoveAll(n => n.OwnerId == ownerId);
            return notes;
        }

        public Note DeleteNoteByNoteIdAndOwnerId(Guid noteId, Guid ownerId)
        {
            var note = _notes.FirstOrDefault(n => n.Id == noteId && n.OwnerId == ownerId);
            if (note == null)
                return null;
            _notes.Remove(note);
            return note;
        }

        public Note Get(Guid Id)
        {
            return _notes.First(n => n.Id == Id);
        }

        public List<Note> GetAll()
        {
            return _notes;
        }

        public List<Note> GetNotesByOwnerId(Guid id)
        {
            return _notes.Where(n => n.OwnerId == id).ToList();
        }

        public Note Update(Guid id, Note model)
        {
            var noteIndex = _notes.FindIndex(n => n.Id == model.Id);
            if (noteIndex == -1)
                return null;
            model.Id = id;
            _notes[noteIndex] = model;
            return model;
        }

        public Note UpdateNoteByNoteIdAndOwnerId(Guid noteId, Guid ownerId, Note note)
        {
            var noteIndex = _notes.FindIndex(n => n.Id == noteId && n.OwnerId == ownerId);
            if (noteIndex == -1)
                return null;
            note.Id = noteId;
            note.OwnerId = ownerId;
            _notes[noteIndex] = note;
            return note;
            
        }
    }
}
