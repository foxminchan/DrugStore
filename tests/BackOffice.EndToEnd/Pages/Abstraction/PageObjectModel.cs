namespace BackOffice.EndToEnd.Pages.Abstraction;

public abstract class PageObjectModel(IPage page)
{
    public static string BaseUrl => ConfigurationHelper.GetBaseUrl();

    public abstract string PagePath { get; }

    public abstract IBrowser Browser { get; }

    public TPage As<TPage>() where TPage : PageObjectModel => (TPage)this;

    public async Task RefreshAsync() => await page.ReloadAsync();

    public async Task GotoAsync()
    {
        page = await Browser.NewPageAsync();
        await page.GotoAsync(PagePath);
    }

    public async Task<bool> WaitForConditionAsync(
        Func<Task<bool>> condition,
        bool waitForValue = true,
        int checkDelayMs = 100,
        int numberOfChecks = 300)
    {
        var value = !waitForValue;
        for (var i = 0; i < numberOfChecks; i++)
        {
            value = await condition();
            if (value == waitForValue)
            {
                break;
            }

            await Task.Delay(checkDelayMs);
        }

        return value;
    }
}