
using System;

namespace TodoApp.Models.Requests;

public class NewTodo
{
    public string Summary { get; set; } = string.Empty;

    public string Detail { get; set; } = string.Empty;

    public DateTime? DeadLine { get; set; } = null;
}
