#if NETCOREAPP2_0
using System.Reflection;
using System.Runtime.Loader;

namespace BreakDotNet
{
    public class CustomLoadContext : AssemblyLoadContext
    {
        protected override Assembly Load(AssemblyName assemblyName)
        {
            return null;
        }
    }
}
#endif
