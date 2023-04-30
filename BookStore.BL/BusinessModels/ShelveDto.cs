using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.BL.BusinessModels
{
    public class ShelveDto : BaseDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int RackId { get; set; }
    }
}
