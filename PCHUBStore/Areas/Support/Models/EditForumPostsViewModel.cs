using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Support.Models
{
    public class EditForumPostsViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string Id { get; set; }

        public string CreationDate { get; set; }
        public string PostPicture { get; set; }
    }
}
