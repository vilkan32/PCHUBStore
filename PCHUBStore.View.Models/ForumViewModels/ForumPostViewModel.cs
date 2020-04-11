using System;
using System.Collections.Generic;
using System.Text;

namespace PCHUBStore.View.Models.ForumViewModels
{
    public class ForumPostViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }

        public string CreationDate { get; set; }

        public string Content { get; set; }
        public string PictureUrl { get; set; }

    }
}
