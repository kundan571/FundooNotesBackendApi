using FundooNotes.Entity;
using Microsoft.EntityFrameworkCore;
//using Microsoft.Azure.Cosmos;

namespace FundooNotes.Context;

public class FundooContext : DbContext
{
    public FundooContext(DbContextOptions options) : base(options)
    {
        
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Notes> Notes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasNoDiscriminator().HasManualThroughput(400).HasKey(x => x.EmailId);
        modelBuilder.Entity<User>().ToContainer("UserContainer").HasPartitionKey(x => x.EmailId);
        modelBuilder.Entity<Notes>().HasNoDiscriminator().HasManualThroughput(600).HasKey(x => x.NoteId);
        modelBuilder.Entity<Notes>().ToContainer("NotesContainer").HasPartitionKey(x => x.EmailId);
    }
}
