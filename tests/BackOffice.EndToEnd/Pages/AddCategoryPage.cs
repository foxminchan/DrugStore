using BackOffice.EndToEnd.Pages.Abstraction;

namespace BackOffice.EndToEnd.Pages;

public sealed class AddCategoryPage(IPage page, IBrowser browser) : PageObjectModel
{
    public override string PagePath => $"{BaseUrl}/categories/add";
    public override IBrowser Browser { get; } = browser;
    public override IPage Page { get; set; } = page;

    public Task SetName(string name) => Page.FillAsync("#InputName", name);

    public Task SetDescription(string description) => Page.FillAsync("#InputDescription", description);

    public Task ClickSubmit() => Page.Locator("button[type=\"submit\"]").ClickAsync();

    public Task<bool> IsErrorDisplayed() => Page.IsVisibleAsync("#InputName:invalid");
}