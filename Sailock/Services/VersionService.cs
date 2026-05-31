using System.Reflection;

namespace Sailock.Services
{
    public static class VersionService
    {
        public static string Current =>
            Assembly.GetExecutingAssembly()
                    .GetName()
                    .Version
                    ?.ToString(3) // Major.Minor.Patch
            ?? "1.0.0";
    }
}