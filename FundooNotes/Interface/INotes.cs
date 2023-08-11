using FundooNotes.Entity;
using FundooNotes.Models;

namespace FundooNotes.Interface;

public interface INotes
{
    Notes AddNote(string emailId, NoteModel newNote);
    Notes ViewById(string emailId, string noteId);
    IEnumerable<Notes> ViewAllNotes(string emailId);
    Notes EditNote(string emailId, string noteId, NoteModel updatedNode);
    bool ArchieveNote(string emailId, string noteId);
    bool PinNote(string emailId, string noteId);
    bool TrashNote(string emailId, string noteId);
    bool DeleteNote(string emailId, string noteId);
    bool AddColour(string emailId, string noteId, string color);
}
