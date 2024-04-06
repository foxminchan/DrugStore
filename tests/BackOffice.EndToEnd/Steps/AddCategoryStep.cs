using BackOffice.EndToEnd.Pages;
using BoDi;

namespace BackOffice.EndToEnd.Steps;

[Binding]
public sealed class AddCategoryStep(AddCategoryPage addCategory)
{
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

    [Given("I am on the Add Category page")]
    public async Task GivenIAmOnTheAddCategoryPage() => await addCategory.GotoAsync();

    [When("I enter the category name")]
    public async Task WhenIEnterTheCategoryName()
    {
        await addCategory.SetName("Drugs");
        await addCategory.ClickSubmit();
    }

    [Then("The category should be added")]
    public async Task ThenTheCategoryShouldBeAdded() => throw new NotImplementedException();

    [When("I enter the category description")]
    public async Task WhenIEnterTheCategoryDescription()
    {
        await addCategory.SetDescription("Drugs for sale");
        await addCategory.ClickSubmit();
    }

    [Then("An error message should be displayed")]
    public async Task ThenAnErrorMessageShouldBeDisplayed()
    {
        var isErrorDisplayed = await addCategory.IsErrorDisplayed();
        isErrorDisplayed.Should().BeTrue();
    }

    [When("I enter the category name and description")]
    public async Task WhenIEnterTheCategoryNameAndDescription()
    {
        await addCategory.SetName("Drugs");
        await addCategory.SetDescription("Drugs for sale");
        await addCategory.ClickSubmit();
    }

    [Then("The category should be added successfully")]
    public async Task ThenTheCategoryShouldBeAddedSuccessfully() => throw new NotImplementedException();

    [AfterFeature("AddCategory")]
    public static async Task AfterAddCategoryScenario(IObjectContainer container)
    {
        var browser = container.Resolve<IBrowser>();
        var playwright = container.Resolve<IPlaywright>();
        await browser.CloseAsync();
        playwright.Dispose();
    }
}