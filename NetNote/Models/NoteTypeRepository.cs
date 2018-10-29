using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetNote.Models
{
    public class NoteTypeRepository : INoteTypeRepository
    {
        private readonly NoteContext _noteContext;

        public NoteTypeRepository(NoteContext noteContext)
        {
            _noteContext = noteContext;
        }

        public Task<List<NoteType>> ListAsync()
        {
            return _noteContext.NoteTypes.ToListAsync();
        }
    }
}