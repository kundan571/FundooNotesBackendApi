namespace FundooNotes.Models;

public class NoteModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsPinned { get; set; }
    public bool IsTrashed { get; set; }
    public bool IsArchived { get; set; }
    public string Colour { get; set; }
    public DateTime Reminder { get; set; }
    public List<string> Labels { get; set; } = new();
    public List<string> Collabs { get; set; } = new();
}
