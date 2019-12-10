using System;
using System.Collections.Generic;

namespace ToDo.Domain
{
    public class User:BaseEntity
    {
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }


        public IEnumerable<ToDo> ToDo => _toDo;
        protected readonly ICollection<ToDo> _toDo; //set by EF

        public User()
        {
            _toDo = new List<ToDo>();
        }
    }
}
