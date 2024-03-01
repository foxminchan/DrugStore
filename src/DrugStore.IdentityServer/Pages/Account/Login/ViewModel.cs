// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

namespace DrugStore.IdentityServer.Pages.Account.Login;

public class ViewModel
{
    public bool AllowRememberLogin { get; set; } = true;
    public bool EnableLocalLogin { get; set; } = true;

    public IEnumerable<ExternalProvider> ExternalProviders { get; set; } = [];

    public IEnumerable<ExternalProvider> VisibleExternalProviders =>
        ExternalProviders.Where(x => !string.IsNullOrWhiteSpace(x.DisplayName));

    public bool IsExternalLoginOnly => EnableLocalLogin && ExternalProviders?.Count() == 1;

    public string ExternalLoginScheme =>
        IsExternalLoginOnly ? ExternalProviders?.SingleOrDefault()?.AuthenticationScheme : null;

    public class ExternalProvider
    {
        public string DisplayName { get; set; }
        public string AuthenticationScheme { get; set; }
    }
}