namespace BackOffice.EndToEnd.Steps;

[Binding]
public sealed class AddCustomerStep(AddCustomerPage addCustomerPage)
{
    [BeforeFeature("AddCustomer")]
    public static async Task BeforeAddCustomerScenario(IObjectContainer container)
    {
        var playwright = await Playwright.CreateAsync();

        var options = new BrowserTypeLaunchOptions
        {
            Headless = ConfigurationHelper.GetHeadless(),
            SlowMo = ConfigurationHelper.GetSlowMoMilliseconds()
        };

        var browser = await playwright.Chromium.LaunchAsync(options);
        var page = await browser.NewPageAsync();

        var addCustomerPage = new AddCustomerPage(page, browser);

        container.RegisterInstanceAs(addCustomerPage);
        container.RegisterInstanceAs(options);
        container.RegisterInstanceAs(addCustomerPage);
    }

    [Given("a logged in user on the customers page")]
    public async Task GivenALoggedInUser() => await addCustomerPage.Login();

    [When("user add a customer with valid data")]
    public async Task WhenIAddACustomerWithValidData()
    {
        await addCustomerPage.SetFullName("Nguyen Dinh Anh");
        await addCustomerPage.SetPhone("0123456789");
        await addCustomerPage.SetEmail("dinhanh@gmail.com");
        await addCustomerPage.SetStreet("Dong Khoi");
        await addCustomerPage.SetCity("Bien Hoa");
        await addCustomerPage.SetProvince("Dong Nai");
        await addCustomerPage.ClickSetDefaultPassword();
        await addCustomerPage.ClickSave();
        await addCustomerPage.ClickConfirm();
    }

    [Then("the customer is added successfully")]
    public async Task ThenTheCustomerIsAddedSuccessfully()
    {
        var isCustomerAdded = await addCustomerPage.IsCustomerAdded();
        isCustomerAdded.Should().BeTrue();
    }

    [When(
        "user add a customer with full name '(.*)', phone '(.*)', email '(.*)', street '(.*)', city '(.*)', province '(.*)', password '(.*)' and confirm password '(.*)'")]
    public async Task WhenIAddACustomerWithInvalidData(string fullName, string phone, string email, string street,
        string city, string province, string password, string confirmPassword)
    {
        await addCustomerPage.SetFullName(fullName);
        await addCustomerPage.SetPhone(phone);
        await addCustomerPage.SetEmail(email);
        await addCustomerPage.SetStreet(street);
        await addCustomerPage.SetCity(city);
        await addCustomerPage.SetProvince(province);
        await addCustomerPage.SetPassword(password);
        await addCustomerPage.SetConfirmPassword(confirmPassword);
        await addCustomerPage.ClickSave();
        await addCustomerPage.ClickConfirm();
    }

    [Then("customer error message should be displayed")]
    public async Task ThenCustomerErrorMessageShouldBeDisplayed()
    {
        var isCustomerError = await addCustomerPage.IsCustomerError();
        isCustomerError.Should().BeTrue();
    }
}