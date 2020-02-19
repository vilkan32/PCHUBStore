﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PCHUBStore.Data.Models
{
    public class Picture : BaseModel<int>
    {
        public string Url { get; set; }

        public string Name { get; set; }

        public bool IsVideo { get; set; }
        public int? ProductId { get; set; }
        public Product Product { get; set; }

        [InverseProperty("Pictures")]
        public User Uploader { get; set; } 
        public virtual string RedirectTo { get; set; }
    }
}