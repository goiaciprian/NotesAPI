using MongoDB.Bson;
using MongoDB.Driver;
using Notes_API.Models;
using Notes_API.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notes_API.Services
{
    public class NoteCollectionService : INoteCollectionService
    {
        private readonly IMongoCollection<Note> _notes;

        public NoteCollectionService(IMongoDBSettings mongoSettings)
        {
            var client = new MongoClient(mongoSettings.ConnectionString);
            var database= client.GetDatabase(mongoSettings.DatabaseName);

            _notes = database.GetCollection<Note>(mongoSettings.NoteCollectionName);
        }


        public async Task<Note> Create(Note model)
        {
            model.Id = Guid.NewGuid();
            await _notes.InsertOneAsync(model);
            return model;
        }

        public async Task<Note> Delete(Guid id)
        {
            return await _notes.FindOneAndDeleteAsync(n => n.Id == id);
        }

        public async Task<List<Note>> DeleteAllByOwnerId(Guid ownerId)
        {
            var notes = await _notes.FindAsync(n => n.OwnerId == ownerId);
            await _notes.DeleteManyAsync(n => n.OwnerId == ownerId);
            return notes.ToList();
        }

        public async Task<Note> DeleteNoteByNoteIdAndOwnerId(Guid noteId, Guid ownerId)
        {
            return await _notes.FindOneAndDeleteAsync(n => n.Id == noteId && n.OwnerId == ownerId);
        }

        public async Task<Note> Get(Guid Id)
        {
            return await (await _notes.FindAsync(n => n.Id == Id)).FirstOrDefaultAsync();
        }

        public async Task<List<Note>> GetAll()
        {
            return (await _notes.FindAsync(n => true)).ToList();
        }

        public async Task<List<Note>> GetNotesByOwnerId(Guid id)
        {
            return (await _notes.FindAsync(n => n.OwnerId == id)).ToList();
        }

        public async Task<Note> Update(Guid id, Note model)
        {
            return await _notes.FindOneAndReplaceAsync(n => n.Id == id, model);
        }

        public async Task<Note> UpdateNoteByNoteIdAndOwnerId(Guid noteId, Guid ownerId, Note note)
        {
            return await _notes.FindOneAndReplaceAsync(n => n.Id == noteId && n.OwnerId == ownerId, note);

        }
    }
}
