using BackOffice.EndToEnd.Fixtures;

namespace BackOffice.EndToEnd.Pages;

public sealed class AddCustomerPage(IPage page, IBrowser browser) : PageObjectModel
{
    public override string PagePath => $"{BaseUrl}/customers/add";
    public override IBrowser Browser { get; } = browser;
    public override IPage Page { get; set; } = page;

    public async Task SetFullName(string fullName)
        => await Page.GetByPlaceholder("Nguyen Van A").FillAsync(fullName);

    public async Task SetPhone(string phone)
        => await Page.GetByPlaceholder("00000000000").FillAsync(phone);

    public async Task SetEmail(string email)
        => await Page.GetByPlaceholder("example@email.com").FillAsync(email);

    public async Task SetStreet(string street)
        => await Page.GetByPlaceholder("Nam Ky Khoi Nghia").FillAsync(street);

    public async Task SetCity(string city)
        => await Page.GetByPlaceholder("District").FillAsync(city);

    public async Task SetProvince(string province)
        => await Page.GetByPlaceholder("Ho Chi Minh").FillAsync(province);

    public async Task ClickSetDefaultPassword()
        => await Page.Locator(".rz-chkbox-box").ClickAsync();

    public async Task SetPassword(string password)
        => await Page.FillAsync("#InputPassword", password);

    public async Task SetConfirmPassword(string confirmPassword)
        => await Page.FillAsync("#InputConfirmPassword", confirmPassword);

    public async Task ClickSave()
        => await Page.GetByRole(AriaRole.Button, new() { Name = "save Save" }).ClickAsync();

    public async Task ClickConfirm()
        => await Page.GetByRole(AriaRole.Button, new() { Name = "Ok" }).ClickAsync();

    public async Task<bool> IsCustomerAdded()
        => await Page.GetByText("Customer added successfully!").IsVisibleAsync();

    public async Task<bool> IsCustomerError()
        => await Page.Locator(".rz-message.rz-messages-error").IsVisibleAsync();
}