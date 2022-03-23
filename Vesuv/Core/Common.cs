using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Vesuv.Core
{
    public static class Common
    {
        public static readonly string GlobalConfigPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Vesuv");
        public static readonly string GlobalConfigFilePath = Path.Combine(GlobalConfigPath, "Vesuv.ini");

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
