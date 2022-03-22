using Notes_API.Models;
using System;
using System.Collections.Generic;

namespace Notes_API.Services
{
    public interface INoteCollectionService: ICollectionService<Note>
    {
        List<Note> GetNotesByOwnerId(Guid id);
        Note UpdateNoteByNoteIdAndOwnerId (Guid noteId, Guid ownerId, Note note);
        Note DeleteNoteByNoteIdAndOwnerId(Guid noteId, Guid ownerId);
        List<Note> DeleteAllByOwnerId (Guid ownerId);
    }
}
