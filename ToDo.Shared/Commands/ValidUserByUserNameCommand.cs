using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo.Shared.Commands
{
    public class ValidUserByUserNameCommand
    {
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}
