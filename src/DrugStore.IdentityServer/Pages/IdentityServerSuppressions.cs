// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppression either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

// global/shared
[assembly:
    SuppressMessage("Design", "CA1054:URI-like parameters should not be strings",
        Justification = "Consistent with the IdentityServer APIs")]
[assembly:
    SuppressMessage("Design", "CA1056:URI-like properties should not be strings",
        Justification = "Consistent with the IdentityServer APIs")]
[assembly:
    SuppressMessage("Reliability", "CA2007:Consider calling ConfigureAwait on the awaited task",
        Justification =
            "No need for ConfigureAwait in ASP.NET Core application code, as there is no SynchronizationContext.")]
[assembly:
    SuppressMessage("Naming", "CA1724:Type names should not match namespaces",
        Justification =
            "Namespaces of pages are not likely to be used elsewhere, so there is little chance of confusion",
        Scope = "type", Target = "~T:DrugStore.IdentityServer.Pages.Ciba.Consent")]
[assembly:
    SuppressMessage("Naming", "CA1724:Type names should not match namespaces",
        Justification =
            "Namespaces of pages are not likely to be used elsewhere, so there is little chance of confusion",
        Scope = "type", Target = "~T:DrugStore.IdentityServer.Pages.Extensions")]