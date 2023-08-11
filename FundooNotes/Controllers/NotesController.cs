using FundooNotes.Entity;
using FundooNotes.Interface;
using FundooNotes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FundooNotes.Controllers;

public class NotesController : Controller
{
    private readonly INotes _notes;
    private readonly ILogger<NotesController> _logger;
    public NotesController(INotes notes, ILogger<NotesController> logger)
    {
        _notes = notes;
        _logger = logger;
    }
    [Authorize]
    [HttpPost]
    [Route("Add")]
    public IActionResult AddNote(NoteModel newNote)
    {
        try
        {
            string emailId = User.FindFirstValue(ClaimTypes.Email);
            Notes note = _notes.AddNote(emailId, newNote);
            if (note != null)
            {
                return Ok(new { success = true, message = "Note Added Successfully", data = note });
            }
            else
            {
                return BadRequest(new { success = false, message = "smething went wrong", });
            }
        }
        catch(Exception ex) 
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
    [Authorize]
    [HttpGet]
    [Route("ViewAll")]
    public IActionResult ViewAllNotes()
    {
        try
        {
            string emailId = User.FindFirstValue(ClaimTypes.Email);
            IEnumerable<Notes> allNotes = _notes.ViewAllNotes(emailId);

            if(allNotes != null)
            {
                return Ok(new {success = true, message = "Note Retrived successfully", data = allNotes});
            }
            else
            {
                return BadRequest(new { success = false, message = "Something went wrong" });
            }
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }

    }
    [Authorize]
    [HttpGet]
    [Route("ViewNoteById")]
    public IActionResult ViewNoteById(string noteId)
    {
        try
        {
            string emailId = User.FindFirstValue(ClaimTypes.Email);
            Notes note = _notes.ViewById(emailId, noteId);
            if (note != null)
            {
                return Ok(new { success = true, message = "successfully retrived", note = note });
            }
            else
            {
                return BadRequest(new { success = false, message = "Something went wrong" });
            }
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
    [Authorize]
    [HttpPut]
    [Route("Edit")]
    public IActionResult EditNote(string noteId, NoteModel newNote)
    {
        try
        {
            string emailId = User.FindFirstValue(ClaimTypes.Email);
            Notes note = _notes.EditNote(emailId, noteId, newNote);
            if (note != null)
            {
                return Ok(new { success = true, message = "Note updated successfully", data = note});
            }
            else
            {
                return BadRequest(new { success = false, message = "something went wrong" });
            }
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
    [Authorize]
    [HttpPut]
    [Route("Pin")]
    public IActionResult PinNote(string noteId)
    {
        try
        {
            string emailId = User.FindFirstValue(ClaimTypes.Email);
            bool result = _notes.PinNote(emailId, noteId);
            if (result)
            {
                return Ok(new { success = true, message = "Note Pinned successfully" });
            }
            else
            {
                return BadRequest(new { success = false, message = "something went wrong" });
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
    [Authorize]
    [HttpPut]
    [Route("Archive")]
    public IActionResult ArchiveNote(string noteId)
    {
        try
        {
            string emailId = User.FindFirstValue(ClaimTypes.Email);
            bool result = _notes.ArchieveNote(emailId, noteId);
            if (result)
            {
                return Ok(new { success = true, message = "Note Archived successfully" });
            }
            else
            {
                return BadRequest(new { success = false, message = "Something went wrong" });
            }
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    [Authorize]
    [HttpPut]
    [Route("Trash")]
    public IActionResult TrashNote(string noteId)
    {
        try
        {
            string emailId = User.FindFirstValue(ClaimTypes.Email);
            bool result = _notes.TrashNote(emailId, noteId);
            if (result)
            {
                return Ok(new { success = true, message = "Note Trashed successfully" });
            }
            else
            {
                return BadRequest(new { success = false, message = "Something went wrong" });
            }
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    [Authorize]
    [HttpPut]
    [Route("Colour")]
    public IActionResult ChangeColour(string noteId,  string colour)
    {
        try
        {
            string emailId = User.FindFirstValue(ClaimTypes.Email);
            bool result = _notes.AddColour(emailId, noteId, colour);
            if (result)
            {
                return Ok(new { success = true, message = "Colour changed successfully" });
            }
            else
            {
                return BadRequest(new { success = false, message = "Somethimg went wrong" });
            }
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;

        }
    }
    [Authorize]
    [HttpDelete]
    [Route("Delete")]
    public IActionResult DeleteNote(string noteId)
    {
        try
        {
            string emailId = User.FindFirstValue(ClaimTypes.Email);
            bool result = _notes.DeleteNote(emailId, noteId);
            if (result)
            {
                return Ok(new { success = true, message = "Note deleted successfully" });
            }
            else
            {
                return BadRequest(new { success = false, message = "Something went wrong" });
            }
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
}
