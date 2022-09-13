using System.Collections.Specialized;
using System.Linq;
using System.Web.Mvc;
using TodoApp.Data;
using BC = BCrypt.Net.BCrypt;

namespace TodoApp.Providers;

#nullable enable
public class TodoAppMembershipProvider : AbandonMembershipProvider
{
    private TodoDbContext? _dbContext;


    public override void Initialize(string name, NameValueCollection config)
    {
        base.Initialize(name, config);
        _dbContext = DependencyResolver.Current.GetService<TodoDbContext>();
    }

    public override bool ValidateUser(string username, string password)
    {
        var user = _dbContext?.Users.FirstOrDefault(u => string.Equals(u.Username, username));
        if (user == null)
        {
            return false;
        }

        return BC.Verify(password, user.HashedPassword);
    }

}
