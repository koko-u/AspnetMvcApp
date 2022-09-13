
using System.Data.Entity;
using TodoApp.Models;

namespace TodoApp.Data;

#nullable enable
public class TodoDbContext : DbContext
{
    public TodoDbContext() : base("TodoDbConnectionString")
    {

    }
    public DbSet<Todo> Todoes => Set<Todo>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
}
