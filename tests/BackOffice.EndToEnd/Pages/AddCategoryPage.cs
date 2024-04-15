using BackOffice.EndToEnd.Fixtures;

namespace BackOffice.EndToEnd.Pages;

public sealed class AddCategoryPage(IPage page, IBrowser browser) : PageObjectModel
{
    public override string PagePath => $"{BaseUrl}/categories";
    public override IBrowser Browser { get; } = browser;
    public override IPage Page { get; set; } = page;

    public async Task ClickAddCategory() 
        => await Page.GetByRole(AriaRole.Button, new() { Name = "add_circle_outline Add" }).ClickAsync();

    public async Task SetName(string name) 
        => await Page.GetByPlaceholder("Vitamins and Minerals", new() { Exact = true }).FillAsync(name);

    public async Task SetDescription(string description)
        => await Page.GetByPlaceholder("A description of Vitamins and").FillAsync(description);

    public async Task ClickSave()
        => await Page.GetByRole(AriaRole.Button, new() { Name = "save Save" }).ClickAsync();

    public async Task ClickConfirm()
        => await Page.GetByRole(AriaRole.Button, new() { Name = "Ok" }).ClickAsync();

    public async Task<bool> IsCategoryAdded()
        => await Page.GetByText("Category added successfully!").IsVisibleAsync();

    public async Task<bool> IsCategoryError()
        => await Page.Locator(".rz-message.rz-messages-error").IsVisibleAsync();
}