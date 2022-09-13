
using System;

namespace TodoApp.Models.Requests;

public class UpdateTodo
{
    public Guid Id { get; }

    public string Summary { get; set; } = string.Empty;

    public string Detail { get; set; } = string.Empty;

    public DateTime? DeadLine { get; set; } = null;

    public bool Done { get; set; } = false;

    public UpdateTodo(Guid id)
    {
        this.Id = id;
    }

}
