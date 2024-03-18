using System.Reflection;

namespace DrugStore.Persistence;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    public static readonly Assembly ExecutingAssembly = Assembly.GetExecutingAssembly();
    public static readonly Assembly DbContextAssembly = typeof(ApplicationDbContext).Assembly;
}