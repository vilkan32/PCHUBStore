using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PCHUBStore.Data.Models
{
    public class ForumPost : BaseModel<string>
    {
        public ForumPost()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
            this.ModificationDate = DateTime.UtcNow;
        }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string PictureUrl { get; set; }
    }
}
