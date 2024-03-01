using System.ComponentModel.DataAnnotations;

namespace DrugStore.IdentityServer.Pages.Account.Register;

public class RegisterViewModel
{
    [Required]
    [EmailAddress]
    [MaxLength(50, ErrorMessage = "Email must be less than 50 characters.")]
    public string Email { get; set; }

    [Required]
    [MaxLength(50, ErrorMessage = "Full name must be less than 50 characters.")]
    public string FullName { get; set; }

    [Required]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits.")]
    public string PhoneNumber { get; set; }

    [Required]
    [MaxLength(50, ErrorMessage = "Street must be less than 50 characters.")]
    public string Street { get; set; }

    [Required]
    [MaxLength(50, ErrorMessage = "City must be less than 50 characters.")]
    public string City { get; set; }

    [Required]
    [MaxLength(50, ErrorMessage = "Province must be less than 50 characters.")]
    public string Province { get; set; }

    [Required]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$",
        ErrorMessage =
            "Password must be 8-15 characters and contain at least one uppercase letter, one lowercase letter, and one digit.")]
    public string Password { get; set; }

    [Required]
    [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

    public string ReturnUrl { get; set; }

    public string Button { get; set; }
}