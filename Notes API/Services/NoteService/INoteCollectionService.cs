using MongoDB.Bson;
using Notes_API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notes_API.Services
{
    public interface INoteCollectionService: ICollectionService<Note>
    {
        Task<List<Note>> GetNotesByOwnerId(Guid id);
        Task<Note> UpdateNoteByNoteIdAndOwnerId (Guid noteId, Guid ownerId, Note note);
        Task<Note> DeleteNoteByNoteIdAndOwnerId(Guid noteId, Guid ownerId);
        Task<List<Note>> DeleteAllByOwnerId (Guid ownerId);
    }
}
