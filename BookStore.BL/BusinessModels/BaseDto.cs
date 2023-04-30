using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStore.BL.BusinessModels
{
    public class BaseDto
    {
        //private const int DefaultIdValue = -1;

        //protected BaseDto()
        //{
        //    Id = DefaultIdValue;
        //}
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public DateTime UpdatedDate { get; set; }
    }
}
