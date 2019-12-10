using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo.Shared.DTO
{
    public class ToDoVm
    {
        public DateTime? DueDate { get; set; }
        public string Note { get; set; }
        public string Title { get; set; }

        public bool Done { get; set; }
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserVm User { get; set; }
    }
}
