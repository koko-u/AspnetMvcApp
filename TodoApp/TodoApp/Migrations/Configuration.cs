using System;
using System.Data.Entity.Migrations;
using System.Linq;
using TodoApp.Data;
using TodoApp.Models;
using BC = BCrypt.Net.BCrypt;

namespace TodoApp.Migrations;

#nullable enable

internal sealed class Configuration : DbMigrationsConfiguration<TodoDbContext>
{
    public Configuration()
    {
        AutomaticMigrationsEnabled = true;
        AutomaticMigrationDataLossAllowed = true;
    }

    protected override void Seed(TodoDbContext dbContext)
    {
        //  This method will be called after migrating to the latest version.

        //  You can use the DbSet<T>.AddOrUpdate() helper extension method
        //  to avoid creating duplicate seed data.

        var users = dbContext.Users;
        if (users.Any())
        {
            dbContext.Users.RemoveRange(users);
        }
        var roles = dbContext.Roles;
        if (roles.Any())
        {
            dbContext.Roles.RemoveRange(roles);
        }

        var admin = new User { Id = Guid.NewGuid(), Username = "admin", HashedPassword = BC.HashPassword("admin") };
        var user1 = new User { Id = Guid.NewGuid(), Username = "user1", HashedPassword = BC.HashPassword("user1") };
        var user2 = new User { Id = Guid.NewGuid(), Username = "user2", HashedPassword = BC.HashPassword("user2") };

        var adminRole = new Role { Id = Guid.NewGuid(), Name = "Administrator" };
        var userRole = new Role { Id = Guid.NewGuid(), Name = "User" };

        admin.Roles.Add(adminRole);
        user1.Roles.Add(userRole);
        user2.Roles.Add(userRole);

        adminRole.Users.Add(admin);
        userRole.Users.Add(user1);
        userRole.Users.Add(user2);

        dbContext.Users.AddOrUpdate(e => e.Id, admin, user1, user2);
        dbContext.Roles.AddOrUpdate(e => e.Id, adminRole, userRole);
        dbContext.SaveChanges();
    }
}

