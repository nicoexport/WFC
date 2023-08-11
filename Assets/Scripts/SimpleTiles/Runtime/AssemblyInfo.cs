using System.Runtime.CompilerServices;
using WFC.SimpleTiles;

[assembly: InternalsVisibleTo(AssemblyInfo.NAMESPACE_EDITOR)]
[assembly: InternalsVisibleTo(AssemblyInfo.NAMESPACE_TESTS)]

namespace WFC.SimpleTiles {
    internal static class AssemblyInfo {
        public const string NAMESPACE_RUNTIME = "WFC.SimpleTiles";
        public const string NAMESPACE_EDITOR = "WFC.SimpleTiles.Editor";
        public const string NAMESPACE_TESTS = "WFC.SimpleTiles.Tests";
    }
}