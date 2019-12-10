using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ToDo.Domain;

namespace P683.Archives.Database
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<ToDo.Domain.ToDo> ToDo { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users", "todo");
            modelBuilder.Entity<ToDo.Domain.ToDo>().ToTable("ToDo", "todo");
      
            base.OnModelCreating(modelBuilder);
        }
    }
}
