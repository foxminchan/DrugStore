using System.Reflection;

namespace DrugStore.Presentation;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    public static readonly Assembly Program = typeof(Program).Assembly;
}