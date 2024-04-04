using DrugStore.BackOffice.Components.Pages.Users.Shared.Requests;

namespace DrugStore.BackOffice.Components.Pages.Users.Profile;

public partial class Index
{
    private bool _busy;

    private bool _error;

    private UpdateUser _user = new();

    private async Task OnSubmit(UpdateUser user)
    {
    }
}