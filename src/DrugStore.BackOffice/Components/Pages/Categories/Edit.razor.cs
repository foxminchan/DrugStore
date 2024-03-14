using DrugStore.BackOffice.Components.Pages.Categories.Requests;
using DrugStore.BackOffice.Components.Pages.Categories.Services;
using DrugStore.BackOffice.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Refit;

namespace DrugStore.BackOffice.Components.Pages.Categories;

public sealed partial class Edit
{
    [Inject] private ICategoriesApi CategoriesApi { get; set; } = default!;

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    [Inject] private NotificationService NotificationService { get; set; } = default!;

    private bool _busy;

    private readonly UpdateCategory _category = new();

    [Parameter] public required string Id { get; set; }

    private Dictionary<string, string> _errors = [];

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _busy = true;
            var category = await CategoriesApi.GetCategoryAsync(new(Id));
            _category.Id = category.Id.ToString();
            _category.Name = category.Name;
            _category.Description = category.Description;
        }
        catch (Exception)
        {
            NotificationService.Notify(new()
                { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Unable to load Category" });
            NavigationManager.NavigateTo("/categories");
        }
        finally
        {
            _busy = false;
        }
    }

    private async Task OnSubmit(UpdateCategory category)
    {
        try
        {
            _busy = true;
            await CategoriesApi.UpdateCategoryAsync(category);
            NotificationService.Notify(new()
            {
                Severity = NotificationSeverity.Success,
                Summary = "Success",
                Detail = "Category updated successfully!"
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