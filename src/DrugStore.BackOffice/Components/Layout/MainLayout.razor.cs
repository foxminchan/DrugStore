using Microsoft.AspNetCore.Components;

namespace DrugStore.BackOffice.Components.Layout;

public sealed partial class MainLayout
{
    private bool _sidebarExpanded = true;

    [Inject] private NavigationManager Navigation { get; set; } = default!;

    private async Task OnLogOut() => Navigation.NavigateTo("/logout");
}