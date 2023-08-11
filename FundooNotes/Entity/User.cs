namespace FundooNotes.Entity;

public class User
{
    public string EmailId { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime RegisterAt { get; set; } = DateTime.Now;
}
