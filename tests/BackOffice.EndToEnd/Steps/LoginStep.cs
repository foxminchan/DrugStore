namespace BackOffice.EndToEnd.Steps;

[Binding]
public sealed class LoginStep(LoginPage loginPage)
{
    [BeforeFeature("Login")]
    public static async Task BeforeLoginScenario(IObjectContainer container)
    {
        var playwright = await Playwright.CreateAsync();

        var options = new BrowserTypeLaunchOptions
        {
            Headless = ConfigurationHelper.GetHeadless(),
            SlowMo = ConfigurationHelper.GetSlowMoMilliseconds()
        };

        var browser = await playwright.Chromium.LaunchAsync(options);
        var page = await browser.NewPageAsync();

        var loginPage = new LoginPage(page, browser);

        container.RegisterInstanceAs(loginPage);
        container.RegisterInstanceAs(options);
        container.RegisterInstanceAs(loginPage);
    }

    [Given("a logged out user on the login page")]
    public async Task GivenALoggedOutUser() => await loginPage.GotoAsync();

    [When("the user logs in with valid credentials")]
    public async Task WhenILogInWithValidCredentials()
    {
        await loginPage.SetEmail("nguyenxuannhan@gmail.com");
        await loginPage.SetPassword("P@ssw0rd");
        await loginPage.ClickSubmit();
    }

    [Then("they log in successfully")]
    public async Task ThenILogInSuccessfully()
    {
        var home = await loginPage.IsDashboardPage();
        home.Should().NotBeNull();
        home.Should().Be("Home");
    }

    [When("the user logs in with invalid credentials like email '(.*)' and password '(.*)'")]
    public async Task WhenILogInWithInvalidCredentials(string email, string password)
    {
        await loginPage.SetEmail(email);
        await loginPage.SetPassword(password);
        await loginPage.ClickSubmit();
    }

    [Then("an error is displayed")]
    public async Task ThenAnErrorMessageShouldBeDisplayed()
    {
        var isErrorDisplayed = await loginPage.IsErrorDisplayed();
        isErrorDisplayed.Should().BeTrue();
    }

    [AfterFeature]
    public static async Task AfterLoginScenario(IObjectContainer container)
    {
        var browser = container.Resolve<IBrowser>();
        var playwright = container.Resolve<IPlaywright>();
        await browser.CloseAsync();
        playwright.Dispose();
    }
}