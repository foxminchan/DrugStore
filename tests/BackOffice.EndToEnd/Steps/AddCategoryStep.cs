namespace BackOffice.EndToEnd.Steps;

[Binding]
public sealed class AddCategoryStep(AddCategoryPage addCategoryPage)
{
    private readonly Faker _faker = new();

    [BeforeFeature("AddCategory")]
    public static async Task BeforeAddCategoryScenario(IObjectContainer container)
    {
        var playwright = await Playwright.CreateAsync();

        var options = new BrowserTypeLaunchOptions
        {
            Headless = ConfigurationHelper.GetHeadless(),
            SlowMo = ConfigurationHelper.GetSlowMoMilliseconds()
        };

        var browser = await playwright.Chromium.LaunchAsync(options);
        var page = await browser.NewPageAsync();

        var addCategoryPage = new AddCategoryPage(page, browser);

        container.RegisterInstanceAs(addCategoryPage);
        container.RegisterInstanceAs(options);
        container.RegisterInstanceAs(addCategoryPage);
    }

    [Given("a logged in user on the categories page")]
    public async Task GivenALoggedInUser() => await addCategoryPage.Login();

    [When("user add a category with valid data")]
    public async Task WhenIAddACategoryWithValidData()
    {
        await addCategoryPage.ClickAddCategory();
        await addCategoryPage.SetName("Test");
        await addCategoryPage.SetDescription("Demo test");
        await addCategoryPage.ClickSave();
        await addCategoryPage.ClickConfirm();
    }

    [Then("the category is added successfully")]
    public async Task ThenTheCategoryIsAddedSuccessfully()
    {
        var isCategoryAdded = await addCategoryPage.IsCategoryAdded();
        isCategoryAdded.Should().BeTrue();
    }

    [When("user add a category with name '(.*)' and description '(.*)'")]
    public async Task WhenIAddACategoryWithInvalidData(string name, string description)
    {
        await addCategoryPage.ClickAddCategory();
        await addCategoryPage.SetName(name);
        await addCategoryPage.SetDescription(description);
        await addCategoryPage.ClickSave();
        await addCategoryPage.ClickConfirm();
    }

    [When("user add a category with long name")]
    public async Task WhenIAddACategoryWithLongName()
    {
        await addCategoryPage.ClickAddCategory();
        await addCategoryPage.SetName(_faker.Lorem.Sentence(100));
        await addCategoryPage.SetDescription("Demo test");
        await addCategoryPage.ClickSave();
        await addCategoryPage.ClickConfirm();
    }

    [When("user add a category with long description")]
    public async Task WhenIAddACategoryWithLongDescription()
    {
        await addCategoryPage.ClickAddCategory();
        await addCategoryPage.SetName("Test");
        await addCategoryPage.SetDescription(_faker.Lorem.Sentence(1000));
        await addCategoryPage.ClickSave();
        await addCategoryPage.ClickConfirm();
    }

    [When("user add a category with long name and description")]
    public async Task WhenIAddACategoryWithLongNameAndDescription()
    {
        await addCategoryPage.ClickAddCategory();
        await addCategoryPage.SetName(_faker.Lorem.Sentence(100));
        await addCategoryPage.SetDescription(_faker.Lorem.Sentence(1000));
        await addCategoryPage.ClickSave();
        await addCategoryPage.ClickConfirm();
    }

    [Then("category error message should be displayed")]
    public async Task ThenAnErrorMessageShouldBeDisplayed()
    {
        var isCategoryError = await addCategoryPage.IsCategoryError();
        isCategoryError.Should().BeTrue();
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