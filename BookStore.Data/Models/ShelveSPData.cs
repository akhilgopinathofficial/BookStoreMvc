using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Data.Models
{
    public class ShelveSPData
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int RackId { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<DateTime> UpdatedDate { get; set; }
    }
}
