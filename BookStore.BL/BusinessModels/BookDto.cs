using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.BL.BusinessModels
{
    public class BookDto : BaseDto
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public bool IsAvailable { get; set; }

        public double Price { get; set; }

        public int ShelfId { get; set; }
        public string ShelfName { get; set; }
    }
}
