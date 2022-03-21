using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Vesuv.Core
{
    public static class Common
    {

        public static Version CurrentEngineVersion {
            get {
                var attribute = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyFileVersionAttribute>();
                if (attribute != null) {
                    return new Version(attribute.Version);
                }
                throw new ApplicationException("No AssemblyFileVersionAttribute found.");
            }
        }
    }
}
