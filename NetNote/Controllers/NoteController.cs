using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NetNote.Models;
using NetNote.ViewModel;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NetNote.Controllers
{
    public class NoteController : Controller
    {
        private readonly INoteRepository _noteRepository;
        private readonly INoteTypeRepository _noteTypeRepository;

        public NoteController(INoteRepository noteRepository, INoteTypeRepository noteTypeRepository)
        {
            _noteRepository = noteRepository;
            _noteTypeRepository = noteTypeRepository;
        }

        public async Task<IActionResult> Index(int pageIndex = 1)
        {
            var pagesize = 10;
            var notes = _noteRepository.PageList(pageIndex, pagesize);
            ViewBag.PageCount = notes.Item2;
            ViewBag.PageIndex = pageIndex;
            //var notes = await _noteRepository.ListAsync();
            return View(notes.Item1);
        }

        public async Task<IActionResult> Add()
        {
            var types = await _noteTypeRepository.ListAsync();
            ViewBag.Types = types.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id.ToString()
            });
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(NoteModel model,[FromServices]IHostingEnvironment env)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string fileName = string.Empty;
            if (model.Attachment!=null)
            {
                fileName = Path.Combine("file", Guid.NewGuid().ToString()+ Path.GetExtension(model.Attachment.FileName));
                using (var stream = new FileStream(Path.Combine(env.WebRootPath, fileName), FileMode.CreateNew))
                {
                    model.Attachment.CopyTo(stream);
                }
            }

            await _noteRepository.AddAsync(new Note()
            {
                Title = model.Title,
                Content = model.Content,
                Create = DateTime.Now,
                TypeId = model.Type,
                Password=model.Password,
                Attachment=fileName
            });

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Detail(int id)
        {
            var note = await _noteRepository.GetByIdAsync(id);
            if (!string.IsNullOrEmpty(note.Password))
            {
                return View();
            }
            return View(note);
        }

        [HttpPost]
        public async Task<IActionResult> Detail(int id,string password)
        {
            var note = await _noteRepository.GetByIdAsync(id);
            if (!note.Password.Equals(password))
            {
                return BadRequest("密码错误，返回重新输入");
            }
            return View(note);
        }
    }
}