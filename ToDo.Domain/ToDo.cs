using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo.Domain
{
    public class ToDo : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime? DueDate {get;set;}
        public string Note { get; set; }
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}
