
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models.Requests;

public class LoginRequest
{
    [DisplayName("ユーザ名")]
    [Required]
    public string Username { get; set; } = string.Empty;

    [DisplayName("パスワード")]
    [Required]
    public string Password { get; set; } = string.Empty;

    public string LoginErrorMessage { get; set; } = string.Empty;
}
