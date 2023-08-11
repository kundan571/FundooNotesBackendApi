using FundooNotes.Context;
using FundooNotes.Entity;
using FundooNotes.Interface;
using FundooNotes.Models;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace FundooNotes.Service;

public class NoteService : INotes
{
    private readonly FundooContext _db;

    public NoteService(FundooContext db)
    {
        _db = db;
    }
    public Notes AddNote(string emailId, NoteModel newNote)
    {
        Notes note = new()
        {
            NoteId = Guid.NewGuid().ToString(),
            EmailId = emailId,
            Title = newNote.Title,
            Description = newNote.Description,
            IsArchived = newNote.IsArchived,
            IsTrashed = newNote.IsPinned,
            IsPinned = newNote.IsPinned,
            Reminder = newNote.Reminder,
            Labels = newNote.Labels,
            Collabs = newNote.Collabs,
            Colour = newNote.Colour,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        };
        
        _db.Notes.Add(note);
        int result = _db.SaveChanges();
        return (result > 0) ? note : null;
    }
    public IEnumerable<Notes> ViewAllNotes(string  emailId)
    {
        IEnumerable<Notes> notesForUser = _db.Notes.Where(x => x.EmailId == emailId);
        return notesForUser;
    }
    public Notes ViewById(string emailId, string noteId)
    {
        Notes notesForUser = _db.Notes.FirstOrDefault(x => x.EmailId == emailId && x.NoteId == noteId);
        return notesForUser;
    }
    public Notes EditNote(string emailId, string noteId, NoteModel updatedNote)
    {
        Notes notesForUser = _db.Notes.FirstOrDefault(x => x.EmailId == emailId && x.NoteId == noteId);
        if(notesForUser != null)
        {
            notesForUser.Title = updatedNote.Title;
            notesForUser.Description = updatedNote.Description;
            notesForUser.IsArchived = updatedNote.IsArchived;
            notesForUser.IsPinned = updatedNote.IsPinned;
            notesForUser.IsTrashed = updatedNote.IsPinned;
            notesForUser.IsTrashed = updatedNote.IsTrashed;
            notesForUser.Reminder = updatedNote.Reminder;
            notesForUser.Colour = updatedNote.Colour;
            notesForUser.Labels = updatedNote.Labels;
            notesForUser.Collabs = updatedNote.Collabs;
            notesForUser.UpdatedAt = DateTime.Now;

            _db.Notes.Update(notesForUser);
            _db.SaveChanges();
        }
        return notesForUser;
    }
    public bool PinNote(string emailId, string noteId)
    {
        Notes notesForUser = _db.Notes.FirstOrDefault(x => x.EmailId == emailId && x.NoteId == noteId);
        if(notesForUser != null)
        {
            notesForUser.IsPinned = !notesForUser.IsPinned;
            _db.Notes.Update(notesForUser);
            _db.SaveChanges();
            return true;
        }
        return false;
    }
    public bool ArchieveNote(string emailId, string noteId)
    {
        Notes notesForUser = _db.Notes.FirstOrDefault(x => x.EmailId == emailId && x.NoteId == noteId);
        if (notesForUser != null)
        {
            notesForUser.IsArchived = !notesForUser.IsArchived;
            _db.Notes.Update(notesForUser);
            _db.SaveChanges();
            return true;
        }
        return false;
    }
    public bool TrashNote(string emailId, string noteId)
    {
        Notes notesForUser = _db.Notes.FirstOrDefault(x => x.EmailId == emailId && x.NoteId == noteId);
        if (notesForUser != null)
        {
            notesForUser.IsTrashed = !notesForUser.IsTrashed;
            _db.Notes.Update(notesForUser);
            _db.SaveChanges();
            return true;
        }
        return false;
    }
    public bool AddColour(string email  , string noteId, string colour)
    {
        Notes notesForUser = _db.Notes.FirstOrDefault(x => x.EmailId == email && x.NoteId == noteId);
        if (notesForUser != null)
        {
            notesForUser.Colour = colour;
            _db.Notes.Update(notesForUser);
            _db.SaveChanges();
            return true;
        }
        return false;
    }
    public bool DeleteNote(string emailId, string noteId)
    {
        Notes notesForUser = _db.Notes.FirstOrDefault(x => x.EmailId == emailId && x.NoteId == noteId);
        if( notesForUser != null)
        {
            _db.Notes.Remove(notesForUser);
            _db.SaveChanges();
            return true;
        }
        return false;
    }
}
