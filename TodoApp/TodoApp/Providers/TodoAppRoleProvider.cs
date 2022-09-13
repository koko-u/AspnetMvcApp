using System.Collections.Specialized;
using System.Linq;
using System.Web.Mvc;
using TodoApp.Data;

namespace TodoApp.Providers;

#nullable enable

public class TodoAppRoleProvider : AbandonRoleProvider
{
    private TodoDbContext? _dbContext;


    public override void Initialize(string name, NameValueCollection config)
    {
        base.Initialize(name, config);
        _dbContext = DependencyResolver.Current.GetService<TodoDbContext>();
    }

    public override string[] GetRolesForUser(string username)
    {
        var user = _dbContext?.Users.FirstOrDefault(u => string.Equals(u.Username, username));
        if (user == null)
        {
            return new string[] { };
        }

        return user.Roles.Select(r => r.Name).ToArray();
    }

    public override bool IsUserInRole(string username, string roleName)
    {
        var role = _dbContext?.Roles.FirstOrDefault(r => string.Equals(r.Name, roleName));
        if (role == null)
        {
            return false;
        }
        return role.Users.Any(u => string.Equals(u.Username, username));
    }

}
