using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Support.Models
{
    public class CreateForumPostViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }

        public List<IFormFile> PostPicture { get; set; }
    }
}
