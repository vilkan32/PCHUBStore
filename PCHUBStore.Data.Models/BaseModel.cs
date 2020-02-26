using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PCHUBStore.Data.Models
{
    public abstract class BaseModel<TKey>
    {
        [Key]
        public TKey Id { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public DateTime? DeletedOn { get; set; }
        public bool IsDeleted { get; set; } = false;

        public DateTime ModificationDate { get; set; } = DateTime.UtcNow;
    }
}
