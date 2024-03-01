using System.Reflection;

namespace DrugStore.Application;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    private static readonly Assembly[] AppDomainAssembly = AppDomain.CurrentDomain.GetAssemblies();
    public static IReadOnlyCollection<Assembly> AppDomainAssemblies => Array.AsReadOnly(AppDomainAssembly);
}