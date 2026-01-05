using System.ComponentModel.DataAnnotations;

namespace BootEshop.ViewModels;

public class LoginViewModel
{
    [System.ComponentModel.DataAnnotations.Required]
    [System.ComponentModel.DataAnnotations.EmailAddress]
    public string Email { get; set; }

    [System.ComponentModel.DataAnnotations.Required]
    [System.ComponentModel.DataAnnotations.DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
    public string Password { get; set; }

    [System.ComponentModel.DataAnnotations.Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
    
    public string? ReturnUrl { get; set; }
}