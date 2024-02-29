using System.ComponentModel.DataAnnotations;

using DrugStore.Domain.IdentityAggregate;

namespace DrugStore.IdentityServer.Pages.Account.Register;

public class RegisterViewModel
{
    [Required]
    public string Email { get; set; }
    public string FullName { get; set; }

    [Required]
    public string PhoneNumber { get; set; }

    [Required]
    public string Street { get; set; }

    [Required]
    public string City { get; set; }

    [Required]
    public string Province { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

    public string ReturnUrl { get; set; }

    public string Button { get; set; }
}
