using System.Reflection;

namespace UStack.Identity.Infrastructure.Extensions
{
    public static class AssemblyReference
    {
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    }
}
