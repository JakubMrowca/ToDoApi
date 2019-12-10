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
    public class ToDoController: ControllerBase
    {
        private readonly ToDoContext _toDoContext;

        public ToDoController(ToDoContext toDoContext)
        {
            _toDoContext = toDoContext;
        }

        [HttpPost]
        [Route("")]
        public async Task<ToDo.Domain.ToDo> Create([FromBody] CreateToDoCommand command)
        {
            var toDo = new ToDo.Domain.ToDo
            {
                UserId = command.UserId,
                DueDate = command.DueDate,
                Note = command.Note,
                Title = command.Title
            };
            _toDoContext.ToDo.Add(toDo);
            await _toDoContext.SaveChangesAsync();
            return toDo;
        }

        [HttpPost]
        [Route("confirm")]
        public async Task<ToDo.Domain.ToDo> Confirm([FromBody] ConfirmToDoCommand command)
        {
            var toDo = await _toDoContext.ToDo.Where(x => x.Id == command.ToDoId && x.Arch == false).FirstOrDefaultAsync();
            toDo.Done = true;
            await _toDoContext.SaveChangesAsync();
            return toDo;
        }

        [HttpPut]
        [Route("")]
        public async Task<ToDo.Domain.ToDo> Edit([FromBody] EditToDoCommand command)
        {
            var toDo = await _toDoContext.ToDo.Where(x => x.Id == command.Id && x.Arch == false).FirstOrDefaultAsync();
            toDo.DueDate = command.DueDate;
            toDo.Note = command.Note;
            toDo.UserId = command.UserId;
            toDo.Title = command.Title;

            await _toDoContext.SaveChangesAsync();
            return toDo;
        }

        [HttpDelete]
        [Route("{toDoId}")]
        public async Task<ToDo.Domain.ToDo> Delete(int toDoId)
        {
            var toDo = await _toDoContext.ToDo.Where(x => x.Id == toDoId && x.Arch == false).FirstOrDefaultAsync();
            toDo.Arch = true;

            await _toDoContext.SaveChangesAsync();
            return toDo;
        }

        [HttpGet]
        [Route("")]
        public async Task<IList<ToDoVm>> GetAll()
        {
            var users = await _toDoContext.ToDo.Where(x => x.Arch == false).Include(x => x.User).Select(x => new ToDoVm
            {
                Id = x.Id,
                Done =x.Done,
                Note = x.Note,
                Title = x.Title,
                DueDate = x.DueDate,
                UserId = x.UserId,
                User = new UserVm {
                    Id = x.User.Id,
                    Email = x.User.Email,
                    LastName = x.User.LastName,
                    FirstName = x.User.FirstName,
                    UserName = x.User.UserName,
                }
            }).ToListAsync();
            return users;
        }

        [HttpGet]
        [Route("{toDoId}")]
        public async Task<ToDoVm> Get(int toDoId)
        {
            var user = await _toDoContext.ToDo.Where(x => x.Arch == false && x.Id == toDoId).Include(x => x.User).Select(x => new ToDoVm
            {
                Id = x.Id,
                Done = x.Done,
                Note = x.Note,
                Title = x.Title,
                DueDate = x.DueDate,
                UserId = x.UserId,
                User = new UserVm
                {
                    Id = x.User.Id,
                    Email = x.User.Email,
                    LastName = x.User.LastName,
                    FirstName = x.User.FirstName,
                    UserName = x.User.UserName,
                }
            }).FirstOrDefaultAsync();
            return user;
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
                ToDo = x.ToDo.Where(y=>y.Arch == false).Select(y => new ToDoVm { DueDate = y.DueDate, Note = y.Note, Title = y.Title, Id = y.Id }).ToList()
            }).FirstOrDefaultAsync();
            user.Token = Guid.NewGuid().ToString();
            return user;
        }
    }
}
