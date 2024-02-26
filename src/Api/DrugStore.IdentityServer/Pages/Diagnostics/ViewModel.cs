// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System.Text;
using System.Text.Json;

using IdentityModel;

using Microsoft.AspNetCore.Authentication;

namespace DrugStore.IdentityServer.Pages.Diagnostics;

public class ViewModel
{
    public ViewModel(AuthenticateResult result)
    {
        AuthenticateResult = result;

        if (result.Properties.Items.ContainsKey("client_list"))
        {
            string encoded = result.Properties.Items["client_list"];
            byte[] bytes = Base64Url.Decode(encoded);
            string value = Encoding.UTF8.GetString(bytes);

            Clients = JsonSerializer.Deserialize<string[]>(value);
        }
    }

    public AuthenticateResult AuthenticateResult { get; }
    public IEnumerable<string> Clients { get; } = new List<string>();
}
