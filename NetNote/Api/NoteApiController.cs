using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetNote.Models;
using NetNote.ViewModel;

namespace NetNote.Api
{
    [Route("api/note")]
    [ApiController]
    public class NoteApiController : ControllerBase
    {
        private readonly INoteRepository _noteRepository;
        private readonly INoteTypeRepository _noteTypeRepository;

        public NoteApiController(INoteRepository noteRepository,INoteTypeRepository noteTypeRepository)
        {
            _noteRepository = noteRepository;
            _noteTypeRepository = noteTypeRepository;
        }

        [HttpGet]
        public IActionResult Get(int pageIndex = 1)
        {
            var pagesize = 10;
            var notes = _noteRepository.PageList(pageIndex, pagesize);
            //ViewBag.PageCount = notes.Item2;
            //ViewBag.PageIndex = pageIndex;
            var result = notes.Item1.Select(r => new NoteViewModel()
            {
                Id=r.Id,
                Title=string.IsNullOrEmpty(r.Password)?r.Title:"加密内容",
                Content=string.IsNullOrEmpty(r.Password)?r.Content:"",
                Attachment=string.IsNullOrEmpty(r.Password)?r.Attachment:"",
                Type=r.Type.Name
            }).ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id,string password)
        {
            var note = await _noteRepository.GetByIdAsync(id);
            if (note == null)
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(note.Password) && !note.Password.Equals(password))
            {
                return Unauthorized();
            }
            var result = new NoteViewModel()
            {
                Id=note.Id,
                Title=note.Title,
                Content=note.Content,
                Attachment=note.Attachment,
                Type=note.Type.Name
            };
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NoteModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string fileName = string.Empty;
            await _noteRepository.AddAsync(new Note
            {
                Title=model.Title,
                Content=model.Content,
                Create=DateTime.Now,
                TypeId=model.Type,
                Password=model.Password,
                Attachment=fileName
            });
            return CreatedAtAction("index", "");
        }
    }
}