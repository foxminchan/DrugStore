using BackOffice.EndToEnd.Fixtures;

namespace BackOffice.EndToEnd.Pages;

public sealed class LoginPage(IPage page, IBrowser browser) : PageObjectModel
{
    public override string PagePath => $"{BaseUrl}/";
    public override IBrowser Browser { get; } = browser;
    public override IPage Page { get; set; } = page;

    public Task SetEmail(string email) => Page.GetByPlaceholder("Username").FillAsync(email);

    public Task SetPassword(string password) => Page.GetByPlaceholder("Password").FillAsync(password);

    public Task ClickSubmit() => Page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();

    public async Task<bool> IsErrorDisplayed() => await Page.IsVisibleAsync(".alert-danger");

    public async Task<string?> IsDashboardPage()
        => await Page.GetByRole(AriaRole.Heading, new() { Name = "Home" }).TextContentAsync();
}