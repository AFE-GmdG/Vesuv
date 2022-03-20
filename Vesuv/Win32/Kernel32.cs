using System.Runtime.InteropServices;
using System.Text;

namespace Vesuv.Win32
{

    public static class Kernel32
    {

        private enum FORMAT_MESSAGE : uint
        {
            ALLOCATE_BUFFER = 0x00000100,
            IGNORE_INSERTS = 0x00000200,
            FROM_SYSTEM = 0x00001000,
            ARGUMENT_ARRAY = 0x00002000,
            FROM_HMODULE = 0x00000800,
            FROM_STRING = 0x00000400
        }

        [DllImport("kernel32.dll", EntryPoint = "FormatMessageW", CharSet = CharSet.Unicode)]
        private static extern int FormatMessage(FORMAT_MESSAGE dwFlags, IntPtr lpSource, int dwMessageId, uint dwLanguageId, out StringBuilder msgOut, int nSize, IntPtr Arguments);


        public static string? GetLastErrorMessage()
        {
            var error = Marshal.GetLastWin32Error();
            if (error == 0) {
                return null;
            }
            var errorMessage = new StringBuilder(256);
            var size = FormatMessage(
                FORMAT_MESSAGE.ALLOCATE_BUFFER | FORMAT_MESSAGE.FROM_SYSTEM | FORMAT_MESSAGE.IGNORE_INSERTS,
                IntPtr.Zero,
                error,
                0,
                out errorMessage,
                errorMessage.Capacity,
                IntPtr.Zero);
            if (size == 0) {
                return null;
            }
            return errorMessage.ToString();
        }

    }
}
