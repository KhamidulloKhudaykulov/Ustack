using System.Reflection;

namespace UStack.Course.Application.Extensions
{
    public static class AssemblyReference
    {
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    }
}
