using System.Reflection;

namespace UStack.Notification.Application.Extensions
{
    public static class AssemblyReference
    {
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    }
}
