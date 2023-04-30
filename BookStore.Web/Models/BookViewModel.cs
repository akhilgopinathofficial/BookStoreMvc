using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Web.Models
{
    public class BookViewModel : BaseViewModel
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public bool IsAvailable { get; set; }

        public double Price { get; set; }

        public int ShelfId { get; set; }

        public string ShelfName { get; set; }
    }
}
