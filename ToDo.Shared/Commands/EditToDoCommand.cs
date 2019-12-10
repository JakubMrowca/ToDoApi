using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo.Shared.Commands
{
    public class EditToDoCommand
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }

        public DateTime DueDate { get; set; }
    }
}
