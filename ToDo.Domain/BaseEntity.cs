using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo.Domain
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public bool Arch { get; set; }
        public DateTime CreateDateTime { get; set; }

          public BaseEntity()
        {
            CreateDateTime = DateTime.Now;
        }

    }
}
