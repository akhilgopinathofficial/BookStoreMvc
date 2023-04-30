using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookStore.Data.Models
{
    public class Book : Base
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public bool IsAvailable { get; set; }

        public double Price { get; set; }

        public int ShelfId { get; set; }
        [ForeignKey("ShelfId")]
        public virtual Shelve Shelve { get; set; }
    }
}
