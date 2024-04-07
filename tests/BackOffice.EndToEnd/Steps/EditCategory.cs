using BackOffice.EndToEnd.Pages;
using BoDi;

namespace BackOffice.EndToEnd.Steps;

[Binding]
public sealed class EditCategory(EditCategoryPage editCategory)
{
    [BeforeFeature("EditCategory")]
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

        var editCategoryPage = new AddCategoryPage(page, browser);

        container.RegisterInstanceAs(editCategoryPage);
        container.RegisterInstanceAs(options);
        container.RegisterInstanceAs(editCategoryPage);
    }

    [Given("I am on the Edit Category page")]
    public async Task GivenIAmOnTheEditCategoryPage() => await editCategory.GotoAsync();

    [When("I enter the category name")]
    public async Task WhenIEnterTheCategoryName()
    {
        await editCategory.SetName("Drugs");
        await editCategory.ClickSubmit();
    }

    [Then("The category should be modified")]
    public async Task ThenTheCategoryShouldBeAdded() => throw new NotImplementedException();

    [When("I enter the category description")]
    public async Task WhenIEnterTheCategoryDescription()
    {
        await editCategory.SetDescription("Drugs for sale");
        await editCategory.ClickSubmit();
    }

    [Then("An error message should be displayed")]
    public async Task ThenAnErrorMessageShouldBeDisplayed()
    {
        var isErrorDisplayed = await editCategory.IsErrorDisplayed();
        isErrorDisplayed.Should().BeTrue();
    }

    [When("I enter the category name and description")]
    public async Task WhenIEnterTheCategoryNameAndDescription()
    {
        await editCategory.SetName("Drugs");
        await editCategory.SetDescription("Drugs for sale");
        await editCategory.ClickSubmit();
    }

    [AfterFeature("EditCategory")]
    public static async Task AfterAddCategoryScenario(IObjectContainer container)
    {
        var browser = container.Resolve<IBrowser>();
        var playwright = container.Resolve<IPlaywright>();
        await browser.CloseAsync();
        playwright.Dispose();
    }
}