using System;
using System.Collections.Generic;
using System.Text;

namespace PCHUBStore.Data.Models
{
    public class MainSlider : BaseModel<int>
    {
        public MainSlider()
        {
            this.MainSliderPictures = new List<Picture>();
        }
        public string Name { get; set; }
        public virtual ICollection<Picture> MainSliderPictures { get; set; }
      
    }
}
