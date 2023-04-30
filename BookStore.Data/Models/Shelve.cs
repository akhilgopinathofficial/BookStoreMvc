using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookStore.Data.Models
{
    public class Shelve : Base
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int RackId { get; set; }
        [ForeignKey("RackId")]
        public virtual Rack Rack { get; set; }

    }
}
