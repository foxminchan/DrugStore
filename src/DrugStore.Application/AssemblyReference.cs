using System.Reflection;

namespace DrugStore.Application;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    public static readonly Assembly Executing = Assembly.GetExecutingAssembly();
}