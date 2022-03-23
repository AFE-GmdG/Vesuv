using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Vesuv.Core.IO
{
    public static class FileSystemExtension
    {

        public static async Task<string?> ReadIni(this Stream stream, string section, string key)
        {
            if (!stream.CanRead) {
                throw new InvalidOperationException($"The stream is not readable");
            }
            if (String.IsNullOrWhiteSpace(section)) {
                throw new ArgumentException("Section only contains white space characters", nameof(section));
            }

            if (String.IsNullOrWhiteSpace(key)) {
                throw new ArgumentException("Key only contains white space characters", nameof(key));
            }

            if (stream.CanSeek) {
                stream.Seek(0, SeekOrigin.Begin);
            }

            var trimmedSection = section.Trim();
            var trimmedKey = key.Trim();

            using (var sr = new StreamReader(stream, Encoding.UTF8, true, 4096, true)) {
                var sectionTester = new Regex(@"^\s*\[(\w+)\]$", RegexOptions.IgnoreCase|RegexOptions.Singleline|RegexOptions.Compiled);
                var keyTester = new Regex(@"^([\w\s]+)=(.*)?$", RegexOptions.IgnoreCase|RegexOptions.Singleline|RegexOptions.Compiled);
                // Find the section
                var foundSection = false;
                while (!sr.EndOfStream) {
                    var line = await sr.ReadLineAsync();
                    var match = sectionTester.Match(line ?? "");
                    if (match.Success && trimmedSection.Equals(match.Captures[1].Value.Trim(), StringComparison.InvariantCultureIgnoreCase)) {
                        foundSection = true;
                        break;
                    }
                }

                // Find the key
                while (foundSection && !sr.EndOfStream) {
                    var line = await sr.ReadLineAsync();
                    var match = keyTester.Match(line ?? "");
                    if (match.Success) {
                        if (trimmedKey.Equals(match.Captures[1].Value.Trim(), StringComparison.InvariantCultureIgnoreCase)) {
                            return match.Captures[2].Value.Trim();
                        }
                        continue;
                    }

                    // Test, if we already in next section
                    match = sectionTester.Match(line ?? "");
                    if (match.Success) {
                        break;
                    }
                }
            }
            return null;
        }
    }
}
