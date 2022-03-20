using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Vesuv.Win32
{

    public class IniFile
    {
        private readonly string iniFileName;
        private readonly string assemblyName = Assembly.GetExecutingAssembly().GetName().Name!;

        public IniFile(string? iniPath = null)
        {
            var fileInfo = new FileInfo(iniPath ?? assemblyName + ".ini");
            iniFileName = fileInfo.FullName;
            if (!fileInfo.Directory!.Exists) {
                fileInfo.Directory.Create();
            }
        }

        [DllImport("kernel32", EntryPoint = "WritePrivateProfileStringW", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern long WritePrivateProfileString(string section, string? key, string? value, string filePath);

        [DllImport("kernel32", EntryPoint = "GetPrivateProfileStringW", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string section, string key, string defaultValue, StringBuilder retVal, int size, string filePath);

        public string? Read(string key, string? section = null)
        {
            var retVal = new StringBuilder(255);
            var size = GetPrivateProfileString(section ?? assemblyName, key, "", retVal, 255, iniFileName);
            if (size == 0) {
                return null;
            }
            return retVal.ToString();
        }

        public string ReadDefault(string key, string defaultValue, string? section = null)
        {
            var retVal = new StringBuilder(255);
            GetPrivateProfileString(section ?? assemblyName, key, defaultValue, retVal, 255, iniFileName);
            return retVal.ToString();
        }

        public void Write(string key, string value, string? section = null)
        {
            if (WritePrivateProfileString(section ?? assemblyName, key, value, iniFileName) == 0) {
                var errorMessage = Kernel32.GetLastErrorMessage();
                if (errorMessage == null) {
                    throw new InvalidOperationException("Unknown error.");
                }
                throw new InvalidOperationException(errorMessage);
            }
        }

        public void DeleteKey(string key, string? section = null)
        {
            if (WritePrivateProfileString(section ?? assemblyName, key, null, iniFileName) == 0) {
                var errorMessage = Kernel32.GetLastErrorMessage();
                if (errorMessage == null) {
                    throw new InvalidOperationException("Unknown error.");
                }
                throw new InvalidOperationException(errorMessage);
            }
        }

        public void DeleteSection(string? section = null)
        {
            if (WritePrivateProfileString(section ?? assemblyName, null, null, iniFileName) == 0) {
                var errorMessage = Kernel32.GetLastErrorMessage();
                if (errorMessage == null) {
                    throw new InvalidOperationException("Unknown error.");
                }
                throw new InvalidOperationException(errorMessage);
            }
        }

        public bool KeyExists(string key, string? section = null)
        {
            return Read(key, section) == null;
        }
    }

}
