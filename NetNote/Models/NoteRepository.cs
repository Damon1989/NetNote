using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetNote.Models
{
    public class NoteRepository : INoteRepository
    {
        private NoteContext _noteContext;

        public NoteRepository(NoteContext noteContext)
        {
            _noteContext = noteContext;
        }

        public Task AddAsync(Note note)
        {
            _noteContext.Notes.Add(note);
            return _noteContext.SaveChangesAsync();
        }

        public Task<Note> GetByIdAsync(int id)
        {
            return _noteContext.Notes.Include(t=>t.Type).FirstOrDefaultAsync(r => r.Id == id);
        }

        public Task<List<Note>> ListAsync()
        {
            return _noteContext.Notes.Include(type => type.Type).ToListAsync();
        }

        public Tuple<List<Note>, int> PageList(int pageIndex, int pagesize)
        {
            var query = _noteContext.Notes.Include(t => t.Type).AsQueryable();
            var count = query.Count();

            var pagecount = count % pagesize == 0 ? count / pagesize : count / pagesize + 1;

            var notes = query.OrderByDescending(r => r.Create)
                .Skip((pageIndex - 1) * pagesize)
                .Take(pagesize)
                .ToList();

            return new Tuple<List<Note>, int>(notes, pagecount);
        }

        public Task UpdateAsync(Note note)
        {
            _noteContext.Entry(note).State = EntityState.Modified;
            return _noteContext.SaveChangesAsync();
        }
    }
}