using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Mapster;
using TodoApp.Data;
using TodoApp.Models;
using TodoApp.Models.Requests;

namespace TodoApp.Controllers;

#nullable enable

public class TodoesController : Controller
{
    private readonly TodoDbContext _dbContext;

    public TodoesController(TodoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // GET: Todoes
    public ActionResult Index()
    {
        var todoes = _dbContext.Todoes.ProjectToType<TodoData>().ToList();
        return View(todoes);
    }

    // GET: Todoes/Details/5
    public ActionResult Details(Guid? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        var todo = _dbContext.Todoes.Find(id)?.Adapt<TodoData>();
        if (todo == null)
        {
            return HttpNotFound();
        }
        return View(todo);
    }

    // GET: Todoes/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: Todoes/Create
    // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
    // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 をご覧ください。
    [HttpPost]
    [ValidateAntiForgeryToken]
    //public ActionResult Create([Bind(Include = "Summary,Detail,DeadLine")] NewTodo todoRequest)
    public ActionResult Create(NewTodo todoRequest)
    {
        if (ModelState.IsValid)
        {

            var todo = new Todo();
            todoRequest.Adapt(todo);
            _dbContext.Todoes.Add(todo);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        return View(todoRequest);
    }

    // GET: Todoes/Edit/5
    public ActionResult Edit(Guid? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        var todo = _dbContext.Todoes.Find(id);
        if (todo == null)
        {
            return HttpNotFound();
        }

        var config = TypeAdapterConfig<Todo, UpdateTodo>
            .NewConfig()
            .ConstructUsing(src => new UpdateTodo(src.Id))
            .Config;
        var adaptToType = todo.BuildAdapter(config).AdaptToType<UpdateTodo>();
        return View(adaptToType);
    }

    // POST: Todoes/Edit/5
    // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
    // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 をご覧ください。
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "Id,Summary,Detail,DeadLine,Done")] UpdateTodo todoRequest)
    {
        if (ModelState.IsValid)
        {
            _dbContext.Entry(todoRequest.Adapt<Todo>()).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(todoRequest);
    }

    // GET: Todoes/Delete/5
    public ActionResult Delete(Guid? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        var todo = _dbContext.Todoes.Find(id);
        if (todo == null)
        {
            return HttpNotFound();
        }
        return View(todo);
    }

    // POST: Todoes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(Guid id)
    {
        var todo = _dbContext.Todoes.Find(id);
        _dbContext.Todoes.Remove(todo);
        _dbContext.SaveChanges();
        return RedirectToAction("Index");
    }

}

