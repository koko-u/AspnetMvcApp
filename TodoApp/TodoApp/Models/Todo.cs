using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApp.Models;

#nullable enable
public class Todo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(255)]
    public string Summary { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string Detail { get; set; } = string.Empty;

    public DateTime? DeadLine { get; set; } = null;

    public bool Done { get; set; } = false;

}
