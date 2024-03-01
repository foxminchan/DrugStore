using System.Transactions;
using DrugStore.Domain.IdentityAggregate;
using DrugStore.Domain.IdentityAggregate.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DrugStore.IdentityServer.Pages.Account.Register;

[SecurityHeaders]
[AllowAnonymous]
public class IndexModel(UserManager<ApplicationUser> userManager) : PageModel
{
    [BindProperty] public RegisterViewModel Input { get; set; }


    public void OnGet(string returnUrl)
    {
        Input = new() { ReturnUrl = returnUrl };
    }

    public async Task<IActionResult> OnPost()
    {
        if (Input.Button != "register")
            return Redirect("~/Account/Login?ReturnUrl=" + Uri.EscapeDataString(Input.ReturnUrl));

        if (!ModelState.IsValid) return Page();

        ApplicationUser user = new()
        {
            UserName = Input.Email,
            Email = Input.Email,
            FullName = Input.FullName,
            PhoneNumber = Input.PhoneNumber,
            Address = new(Input.Street, Input.City, Input.Province)
        };

        using TransactionScope scope = new(TransactionScopeAsyncFlowOption.Enabled);
        try
        {
            var result = await userManager.CreateAsync(user, Input.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, Roles.Customer);
                scope.Complete();
                return Redirect("~/Account/Login?ReturnUrl=" + Uri.EscapeDataString(Input.ReturnUrl));
            }

            foreach (var error in result.Errors) ModelState.AddModelError(string.Empty, error.Description);

            return Page();
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
            return Page();
        }
    }
}