using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P683.Archives.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Domain;
using ToDo.Shared;
using ToDo.Shared.Commands;
using ToDo.Shared.DTO;

namespace ToDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController:ControllerBase
    {
        private readonly ToDoContext _toDoContext;

        public UserController(ToDoContext toDoContext)
        {
            _toDoContext = toDoContext;
        }

        [HttpPost]
        [Route("registry")]
        public async Task<User> RegistryUser([FromBody] CreateUserCommand command)
        {
            var user = new User
            {
                UserName = command.UserName,
                Email = command.Email,
                Password = command.Password,
                PhoneNumber = command.PhoneNumber,
                LastName = command.LastName,
                FirstName = command.FirstName
            };
            _toDoContext.Users.Add(user);
            await _toDoContext.SaveChangesAsync();
            return user;
        }

        [HttpGet]
        [Route("")]
        public async Task<IList<UserVm>> GetAll()
        {
            var users = await _toDoContext.Users.Where(x => x.Arch == false).Include(x => x.ToDo).Select(x => new UserVm
            {
                Id = x.Id,
                Email = x.Email,
                LastName = x.LastName,
                FirstName = x.FirstName,
                UserName = x.UserName,
                ToDo = x.ToDo.Select(y => new ToDoVm { DueDate = y.DueDate, Note = y.Note, Title = y.Title, Id = y.Id }).ToList()
            }).ToListAsync();
            return users;
        }

        [HttpPost]
        [Route("login")]
        public async Task<UserVm> ValidUser([FromBody] ValidUserByUserNameCommand command)
        {
            var user = await _toDoContext.Users.Where(x => x.Arch == false && x.UserName == command.UserName && x.Password == command.Password).Include(x => x.ToDo).Select(x => new UserVm
            {
                Id = x.Id,
                Email = x.Email,
                LastName = x.LastName,
                FirstName = x.FirstName,
                UserName = x.UserName,
                ToDo = x.ToDo.Select(y => new ToDoVm { DueDate = y.DueDate, Note = y.Note, Title = y.Title, Id = y.Id }).ToList()
            }).FirstOrDefaultAsync();
            user.Token = Guid.NewGuid().ToString();
            return user;
        }
    }
}
