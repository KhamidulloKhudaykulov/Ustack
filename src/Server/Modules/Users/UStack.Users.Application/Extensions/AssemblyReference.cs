using System.Reflection;

namespace UStack.Users.Application.Extensions
{
    public static class AssemblyReference
    {
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    }
}
