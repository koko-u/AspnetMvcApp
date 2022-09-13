using System;
using System.ComponentModel;

namespace TodoApp.Models.Requests;

public class TodoData
{
    public Guid Id { get; set; } = Guid.Empty;

    [DisplayName("概要")]
    public string Summary { get; set; } = string.Empty;

    [DisplayName("詳細")]
    public string Detail { get; set; } = string.Empty;

    [DisplayName("締め切り")]
    public DateTime? DeadLine { get; set; } = null;

    [DisplayName("完了？")]
    public bool Done { get; set; } = false;
}
