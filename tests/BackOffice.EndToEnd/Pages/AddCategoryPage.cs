using BackOffice.EndToEnd.Pages.Abstraction;

namespace BackOffice.EndToEnd.Pages;

public sealed class AddCategoryPage(IPage page, IBrowser browser) : PageObjectModel(page)
{
    public override string PagePath => $"{BaseUrl}/categories/add";
    public override IBrowser Browser { get; } = browser;

    public Task SetName(string name) => page.FillAsync("#InputName", name);

    public Task SetDescription(string description) => page.FillAsync("#InputDescription", description);

    public Task ClickSubmit() => page.Locator("button[type=\"submit\"]").ClickAsync();

    public Task<bool> IsErrorDisplayed() => page.IsVisibleAsync("#InputName:invalid");
}