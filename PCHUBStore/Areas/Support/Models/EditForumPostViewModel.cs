using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Support.Models
{
    public class EditForumPostViewModel
    {

        public EditForumPostViewModel()
        {
            this.NewPicture = new List<IFormFile>();
        }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Id { get; set; }
 
        public string CurrentPictureUrl { get; set; }
        public List<IFormFile> NewPicture { get; set; }
    }
}
