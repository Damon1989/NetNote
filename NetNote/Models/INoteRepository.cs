using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetNote.Models
{
    public interface INoteRepository
    {
        Task<Note> GetByIdAsync(int id);

        Task<List<Note>> ListAsync();

        Task AddAsync(Note note);

        Task UpdateAsync(Note note);

        Tuple<List<Note>, int> PageList(int pageIndex, int pagesize);
    }
}