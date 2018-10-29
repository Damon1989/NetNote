using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetNote.Models
{
    public interface INoteTypeRepository
    {
        Task<List<NoteType>> ListAsync();
    }
}