namespace FundooNotes.Entity;

public class Notes
{
    public string NoteId { get; set; }
    public string EmailId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsPinned { get; set; }
    public bool IsTrashed { get; set; }
    public bool IsArchived { get; set; }
    public DateTime Reminder { get; set; }
    public string Colour { get; set; }
    public List<string> Labels { get; set; } = new List<string>();
    public List<string> Collabs { get; set; } = new();
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set;}
}
