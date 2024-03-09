using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.BackOffice.Services;

public sealed class ExportService<T>
{
    public Stream ExportCsv(IQueryable<T> data)
    {
        var columns = GetProperties(data.ElementType);

        var sb = new StringBuilder();

        var keyValuePairs = columns.ToList();
        foreach (var item in data)
            sb.AppendLine(
                string.Join(
                    ",",
                    keyValuePairs.Select(column => $"{GetValue(
                        item ?? throw new InvalidOperationException(),
                        column.Key
                    )}".Trim()).ToArray()
                ));

        var result =
            new FileStreamResult(
                new MemoryStream(
                    Encoding.Default.GetBytes(
                        $"{string.Join(
                            ",",
                            keyValuePairs.Select(c => c.Key))}{Environment.NewLine}{sb}"))
                , "text/csv")
            {
                FileDownloadName = "export.csv"
            };

        return result.FileStream;
    }

    public static IEnumerable<KeyValuePair<string, Type>> GetProperties(Type type)
        => type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanRead && IsSimpleType(p.PropertyType))
            .Select(p => new KeyValuePair<string, Type>(p.Name, p.PropertyType));

    public static bool IsSimpleType(Type type)
    {
        var underlyingType = type.IsGenericType &&
                             type.GetGenericTypeDefinition() == typeof(Nullable<>)
            ? Nullable.GetUnderlyingType(type)
            : type;

        if (underlyingType == typeof(Guid))
            return true;

        var typeCode = Type.GetTypeCode(underlyingType);

        return typeCode switch
        {
            TypeCode.Boolean
                or TypeCode.Byte
                or TypeCode.Char
                or TypeCode.DateTime
                or TypeCode.Decimal
                or TypeCode.Double
                or TypeCode.Int16
                or TypeCode.Int32
                or TypeCode.Int64
                or TypeCode.SByte
                or TypeCode.Single
                or TypeCode.String
                or TypeCode.UInt16
                or TypeCode.UInt32
                or TypeCode.UInt64 => true,
            _ => false
        };
    }

    private static object GetValue(object target, string name)
        => target.GetType().GetProperty(name)?.GetValue(target) ?? throw new InvalidOperationException();
}