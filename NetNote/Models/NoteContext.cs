using Microsoft.EntityFrameworkCore;

namespace NetNote.Models
{
    public class NoteContext : DbContext
    {
        public NoteContext(DbContextOptions<NoteContext> options)
            : base(options)
        {
        }

        public DbSet<Note> Notes { get; set; }
        public DbSet<NoteType> NoteTypes { get; set; }
    }
}