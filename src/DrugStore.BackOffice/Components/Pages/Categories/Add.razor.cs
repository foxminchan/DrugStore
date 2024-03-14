using DrugStore.BackOffice.Components.Pages.Categories.Requests;
using DrugStore.BackOffice.Components.Pages.Categories.Services;
using DrugStore.BackOffice.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Refit;

namespace DrugStore.BackOffice.Components.Pages.Categories;

public sealed partial class Add
{
    [Inject] private ICategoriesApi CategoriesApi { get; set; } = default!;

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    [Inject] private NotificationService NotificationService { get; set; } = default!;

    private bool _busy;

    private readonly CreateCategory _category = new();

    private Dictionary<string, string> _errors = [];

    private async Task OnSubmit(CreateCategory category)
    {
        try
        {
            _busy = true;
            await CategoriesApi.AddCategoryAsync(category, Guid.NewGuid());
            NotificationService.Notify(new()
            {
                Severity = NotificationSeverity.Success,
                Summary = "Success",
                Detail = "Category added successfully!"
            });
            NavigationManager.NavigateTo("/categories");
        }
        catch (ValidationApiException validationException)
        {
            var errorModel = await validationException.GetContentAsAsync<ValidationHelper>();
            if (errorModel?.ValidationErrors is { })
                _errors = errorModel.ValidationErrors.ToDictionary(error => error.Identifier, error => error.Message);
        }
        catch (Exception)
        {
            NotificationService.Notify(new()
                { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Unable to add Category" });
        }
        finally
        {
            _busy = false;
        }
    }

    private static bool ValidateCategoryName(string? name)
        => !string.IsNullOrEmpty(name) && name.Length <= DataLengthHelper.ShortLength;

    private static bool ValidateCategoryDescription(string? description)
    {
        if (string.IsNullOrEmpty(description)) return true;
        return description.Length <= DataLengthHelper.LongLength;
    }

    private async Task CancelButtonClick(MouseEventArgs arg)
    {
        NavigationManager.NavigateTo("/categories");
        await Task.CompletedTask;
    }
}