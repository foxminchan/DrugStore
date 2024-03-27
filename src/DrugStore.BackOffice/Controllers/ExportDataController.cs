using DrugStore.BackOffice.Components.Pages.Categories.Services;
using DrugStore.BackOffice.Components.Pages.Products.Services;
using DrugStore.BackOffice.Components.Pages.Users.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.BackOffice.Controllers;

[Route("/export")]
public sealed class ExportDataController(
    IProductsApi productsApi,
    ICategoriesApi categoriesApi,
    IUserApi usersApi) : ExportController
{
    [HttpGet("categories")]
    [HttpGet("categories/fileName={fileName}")]
    public async Task<FileStreamResult> ExportCategoriesToCsv(string? fileName = null)
    {
        var categories = await categoriesApi.ListCategoriesAsync();
        return ToCsv(ApplyQuery(categories.Categories.AsQueryable(), Request.Query), fileName);
    }

    [HttpGet("products")]
    [HttpGet("products/fileName={fileName}")]
    public async Task<FileStreamResult> ExportProductsToCsv(string? fileName = null)
    {
        var products = await productsApi.ListProductsAsync(new());
        return ToCsv(ApplyQuery(products.Products.AsQueryable(), Request.Query), fileName);
    }

    [HttpGet("users/{role}")]
    [HttpGet("users/{role}/fileName={fileName}")]
    public async Task<FileStreamResult> ExportUsersToCsv(string? fileName = null, string role = "Customer")
    {
        var users = await usersApi.ListUsersAsync(new(), role);
        return ToCsv(ApplyQuery(users.Users.AsQueryable(), Request.Query), fileName);
    }
}
