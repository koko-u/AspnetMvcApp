using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Controllers;

#nullable enable

public class UsersController : Controller
{
    private readonly TodoDbContext _dbContext;

    public UsersController(TodoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // GET: /Users
    public ActionResult Index()
    {
        return View(_dbContext.Users.ToList());
    }

    // GET: /Users/Details/5
    public ActionResult Details(Guid? id)
    {
        if (!id.HasValue)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        var user = _dbContext.Users.Find(id);
        if (user == null)
        {
            return HttpNotFound();
        }
        return View(user);
    }

    // GET: /Users/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: /Users/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "Id,Username,HashedPassword")] User user)
    {
        if (ModelState.IsValid)
        {
            user.Id = Guid.NewGuid();
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(user);
    }

    // GET: /Users/Edit/5
    public ActionResult Edit(Guid? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        var user = _dbContext.Users.Find(id);
        if (user == null)
        {
            return HttpNotFound();
        }
        return View(user);
    }

    // POST: /Users/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "Id,Username,HashedPassword")] User user)
    {
        if (ModelState.IsValid)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(user);
    }

    // GET: /Users/Delete/5
    public ActionResult Delete(Guid? id)
    {
        if (!id.HasValue)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        var user = _dbContext.Users.Find(id);
        if (user == null)
        {
            return HttpNotFound();
        }
        return View(user);
    }

    // POST: /Users/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(Guid id)
    {
        var user = _dbContext.Users.Find(id);
        _dbContext.Users.Remove(user);
        _dbContext.SaveChanges();
        return RedirectToAction("Index");
    }
}
