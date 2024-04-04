// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

namespace DrugStore.IdentityServer.Pages.Account.Login;

public sealed class ViewModel
{
    public bool AllowRememberLogin { get; set; } = true;
    public bool EnableLocalLogin { get; set; } = true;

    public IEnumerable<ExternalProvider> ExternalProviders { get; set; } = [];

    public IEnumerable<ExternalProvider> VisibleExternalProviders =>
        ExternalProviders.Where(x => !string.IsNullOrWhiteSpace(x.DisplayName));

    public bool IsExternalLoginOnly => EnableLocalLogin && ExternalProviders?.Count() == 1;

    public string? ExternalLoginScheme =>
        IsExternalLoginOnly ? ExternalProviders?.SingleOrDefault()?.AuthenticationScheme : null;

    public bool EnableRegister { get; set; } = true;

    public sealed class ExternalProvider(string authenticationScheme, string? displayName = null)
    {
        public string? DisplayName { get; set; } = displayName;
        public string AuthenticationScheme { get; set; } = authenticationScheme;
    }
}