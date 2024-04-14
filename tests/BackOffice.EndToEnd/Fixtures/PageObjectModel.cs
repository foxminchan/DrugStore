namespace BackOffice.EndToEnd.Fixtures;

public abstract class PageObjectModel
{
    public static string BaseUrl => ConfigurationHelper.GetBaseUrl();

    public abstract string PagePath { get; }

    public abstract IBrowser Browser { get; }

    public abstract IPage Page { get; set; }

    public async Task GotoAsync()
    {
        Page = await Browser.NewPageAsync();
        await Page.GotoAsync(PagePath);
    }

    public async Task Login()
    {
        await Page.GotoAsync(PagePath);
        await Page.GetByPlaceholder("Username").FillAsync("nguyenxuannhan@gmail.com");
        await Page.GetByPlaceholder("Password").FillAsync("P@ssw0rd");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();
    }
}