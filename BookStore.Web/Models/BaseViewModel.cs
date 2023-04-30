using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Web.Models
{
    public class BaseViewModel
    {
        private const int DefaultIdValue = -1;

        protected BaseViewModel()
        {
            Id = DefaultIdValue;
        }

        [Key]
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsNew
        {
            get { return Id == DefaultIdValue; }
        }
    }
}
