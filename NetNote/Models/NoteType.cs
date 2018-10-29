using System.Collections.Generic;

namespace NetNote.Models
{
    public class NoteType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Note> Notes { get; set; }
    }
}