using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Administration.Models.IndexPageViewModels
{
    public class EditMainSliderPictures
    {
        public string Url { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public string RedirectTo { get; set; }

        public int Id { get; set; }
    }
}
