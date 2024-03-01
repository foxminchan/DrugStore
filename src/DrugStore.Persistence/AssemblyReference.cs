using System.Reflection;

namespace DrugStore.Persistence;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    public static readonly Assembly DomainAssembly = typeof(ApplicationDbContext).Assembly;
}