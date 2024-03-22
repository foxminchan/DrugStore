using System.Linq.Dynamic.Core;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DrugStore.BackOffice.Controllers;

public class ExportController : Controller
{
    public IQueryable ApplyQuery<T>(IQueryable<T> items, IQueryCollection? query = null, bool keyless = false)
        where T : class
    {
        if (query is null) return items;

        if (query.ContainsKey("$expand"))
        {
            var propertiesToExpand = query["$expand"].ToString().Split(',');
            items = propertiesToExpand.Aggregate(items, (current, p) => current.Include(p));
        }

        var filter = query.ContainsKey("$filter") ? query["$filter"].ToString() : null;

        if (!string.IsNullOrEmpty(filter))
        {
            if (keyless) items = items.AsQueryable();

            items = items.Where(filter);
        }

        if (query.ContainsKey("$orderBy")) items = items.OrderBy(query["$orderBy"].ToString());

        if (query.ContainsKey("$skip")) items = items.Skip(int.Parse(query["$skip"].ToString()));

        if (query.ContainsKey("$top")) items = items.Take(int.Parse(query["$top"].ToString()));

        return query.ContainsKey("$select") ? items.Select($"new ({query["$select"]})") : items;
    }

    public FileStreamResult ToCsv(IQueryable query, string? fileName = null)
    {
        var columns = GetProperties(query.ElementType);
        var sb = new StringBuilder();
        var keyValuePairs = columns.ToList();

        foreach (var item in query)
            sb.AppendLine(
                string.Join(
                    ",",
                    keyValuePairs.Select(column => $"{GetValue(item, column.Key)}".Trim()).ToArray()
                ));

        var result =
            new FileStreamResult(
                new MemoryStream(
                    Encoding.Default.GetBytes($"{string.Join(
                        ",", keyValuePairs.Select(c => c.Key))}{Environment.NewLine}{sb}"
                    )), MediaTypeNames.Text.Csv)
            {
                FileDownloadName = (!string.IsNullOrEmpty(fileName) ? fileName : "Export") + ".csv"
            };

        return result;
    }

    public static object? GetValue(object target, string name) => target.GetType().GetProperty(name)?.GetValue(target);

    public static IEnumerable<KeyValuePair<string, Type>> GetProperties(Type type) =>
        type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanRead && IsSimpleType(p.PropertyType))
            .Select(p => new KeyValuePair<string, Type>(p.Name, p.PropertyType));

    public static bool IsSimpleType(Type type)
    {
        var underlyingType = type.IsGenericType &&
                             type.GetGenericTypeDefinition() == typeof(Nullable<>)
            ? Nullable.GetUnderlyingType(type)
            : type;

        if (underlyingType == typeof(Guid) || underlyingType == typeof(DateTimeOffset))
            return true;

        var typeCode = Type.GetTypeCode(underlyingType);

        return typeCode switch
        {
            TypeCode.Boolean or TypeCode.Byte or TypeCode.Char or TypeCode.DateTime or TypeCode.Decimal or TypeCode.Double or TypeCode.Int16 or TypeCode.Int32 or TypeCode.Int64 or TypeCode.SByte or TypeCode.Single or TypeCode.String or TypeCode.UInt16 or TypeCode.UInt32 or TypeCode.UInt64 => true,
            _ => false
        };
    }
}