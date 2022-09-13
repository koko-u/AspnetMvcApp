using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApp.Models;

#nullable enable

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [StringLength(255)]
    [Index(IsUnique = true)]
    public string Username { get; set; } = string.Empty;

    public string HashedPassword { get; set; } = string.Empty;

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
