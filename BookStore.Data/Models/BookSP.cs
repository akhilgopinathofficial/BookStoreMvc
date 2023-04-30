using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Data.Models
{
    public class BookSP
    {
        public int Id { get; set; }
        public string Code { get; set; }

        public string Name { get; set; }

        public bool IsAvailable { get; set; }

        public decimal Price { get; set; }

        public int ShelfId { get; set; }
        public string ShelfName { get; set; }
         

        public bool IsDeleted { get; set; }

        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<DateTime> UpdatedDate { get; set; }
    }
}
