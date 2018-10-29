using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NetNote.ViewModel
{
    public class NoteModel
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("标题")]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [DisplayName("内容")]
        public string Content { get; set; }

        [DisplayName("类型")]
        public int Type { get; set; }

        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "附件")]
        public IFormFile Attachment { get; set; }
    }
}