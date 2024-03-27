using DrugStore.BackOffice.Components.Pages.Categories.Requests;
using DrugStore.BackOffice.Components.Pages.Categories.Services;
using DrugStore.BackOffice.Constants;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;

namespace DrugStore.BackOffice.Components.Pages.Categories;

public sealed partial class Add
{
    private bool _busy;

    private bool _error;

    private readonly CreateCategory _category = new();

    [Inject] private ICategoriesApi CategoriesApi { get; set; } = default!;

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    [Inject] private NotificationService NotificationService { get; set; } = default!;

    [Inject] private DialogService DialogService { get; set; } = default!;

    private async Task OnSubmit(CreateCategory category)
    {
        try
        {
            if (await DialogService.Confirm(MessageContent.SAVE_CHANGES) == true)
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
        }
        catch (Exception)
        {
            NotificationService.Notify(new()
                { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Unable to add Category" });
            _error = true;
        }
        finally
        {
            _busy = false;
        }
    }

    private async Task CancelButtonClick(MouseEventArgs arg)
    {
        if (await DialogService.Confirm(MessageContent.DISCARD_CHANGES) == true) DialogService.Close();
    }
}