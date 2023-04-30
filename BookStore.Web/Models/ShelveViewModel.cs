using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Web.Models
{
    public class ShelveViewModel: BaseViewModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int RackId { get; set; }
    }
}
