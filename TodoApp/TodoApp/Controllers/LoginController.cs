using System.Web.Mvc;
using System.Web.Security;
using TodoApp.Models.Requests;
using TodoApp.Providers;

namespace TodoApp.Controllers;

[AllowAnonymous]
public class LoginController : Controller
{
    private readonly TodoAppMembershipProvider _membershipProvider;

    public LoginController(TodoAppMembershipProvider membershipProvider)
    {
        _membershipProvider = membershipProvider;
    }

    // GET: /Login
    public ActionResult Index()
    {
        return View("Login", new LoginRequest());
    }

    // POST: /Login
    [HttpPost]
    [ActionName("Index")]
    [ValidateAntiForgeryToken]
    public ActionResult Login([Bind(Include = "Username,Password")] LoginRequest loginRequest)
    {
        if (!ModelState.IsValid)
        {
            return View("Login", loginRequest);
        }

        if (_membershipProvider.ValidateUser(loginRequest.Username, loginRequest.Password))
        {
            FormsAuthentication.SetAuthCookie(loginRequest.Username, createPersistentCookie: false);
            return RedirectToAction("Index", "Todoes");
        }
        else
        {
            loginRequest.LoginErrorMessage = "ユーザ名もしくはパスワードが間違っています。";
            return View("Login", loginRequest);
        }
    }

    // GET : /Logout
    public ActionResult Logout()
    {
        FormsAuthentication.SignOut();
        return RedirectToAction(nameof(Index));
    }
}
