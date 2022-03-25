using System.Globalization;
using System.IO;

using Vesuv.Core.Collections;
using Vesuv.Core.Config;
using Vesuv.Win32;
using Vesuv.Core;

namespace Vesuv.Editor
{

    public class GlobalConfig : ConfigBase
    {
        public static GlobalConfig Instance { get; } = new GlobalConfig();

        public string? GraphicDeviceName { get; set; }
        public string? DefaultProjectPath { get; set; }
        public string Author { get; set; }

        public int MaxMruProjects { get; set; }

        public MRU<string> MruProjects { get; private set; }

#pragma warning disable CS8618 // All non nullable fields are initialized within methods called by the ctor. - Suppress this warning here.
        private GlobalConfig()
        {
            var globalConfigFileInfo = new FileInfo(Common.GlobalConfigFilePath);
            if (!globalConfigFileInfo.Exists) {
                LoadDefaultValues();
                return;
            }
            LoadGlobalConfig();
        }
#pragma warning restore CS8618

        private void LoadDefaultValues()
        {
            if (MruProjects != null) {
                MruProjects.CollectionChanged -= OnConfigChange;
            }

            var defaultProjectPathInfo = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Vesuv"));
            if (!defaultProjectPathInfo.Exists) {
                defaultProjectPathInfo.Create();
            }
            DefaultProjectPath = defaultProjectPathInfo.FullName;
            Author = Environment.UserName;

            MaxMruProjects = 10;
            MruProjects = new MRU<string>(MaxMruProjects);
            MruProjects.CollectionChanged += OnConfigChange;
            IsModified = false;
        }

        private void LoadGlobalConfig()
        {
            if (MruProjects != null) {
                MruProjects.CollectionChanged -= OnConfigChange;
            }

            var globalConfigFile = new IniFile(Common.GlobalConfigFilePath);

            GraphicDeviceName = globalConfigFile.Read("GraphicDeviceName", "Video");

            DefaultProjectPath = globalConfigFile.Read("DefaultProjectPath", "System");
            if (DefaultProjectPath != null) {
                try {
                    if (!Directory.Exists(DefaultProjectPath)) {
                        DefaultProjectPath = null;
                    }
                } catch {
                    // Directory.Exists may throw a exception, if the DefaultProjectPath is invalid.
                    // Ignote the exception and set the value to null.
                    DefaultProjectPath = null;
                }
            }
            if (DefaultProjectPath == null) {
                DefaultProjectPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Vesuv");
                if (!Directory.Exists(DefaultProjectPath)) {
                    Directory.CreateDirectory(DefaultProjectPath);
                }
            }

            Author = globalConfigFile.ReadDefault("Author", Environment.UserName, "System");

            MaxMruProjects = Math.Max(1, Int32.Parse(
                globalConfigFile.ReadDefault("MaxMruProjects", "10", "MruProjects"),
                NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite,
                CultureInfo.InvariantCulture));

            var mruProjects = new List<string>(MaxMruProjects);
            for (int i = 1; i <= MaxMruProjects; ++i) {
                var mruProjectPath = globalConfigFile.Read(i.ToString(), "MruProjects");
                if (mruProjectPath != null) {
                    mruProjects.Add(mruProjectPath);
                }
            }

            MruProjects = new MRU<string>(MaxMruProjects);
            MruProjects.CollectionChanged += OnConfigChange;
            IsModified = false;
        }

        public override void RevertChanges()
        {
            if (!IsModified) {
                return;
            }

            var globalConfigFileInfo = new FileInfo(Common.GlobalConfigFilePath);
            if (!globalConfigFileInfo.Exists) {
                LoadDefaultValues();
                return;
            }
            LoadGlobalConfig();
        }

        public override void SaveChanges()
        {
            var globalConfigFile = new IniFile(Common.GlobalConfigFilePath);

            if (GraphicDeviceName != null) {
                globalConfigFile.Write("GraphicDeviceName", GraphicDeviceName, "Video");
            }

            if (DefaultProjectPath != null) {
                try {
                    if (new DirectoryInfo(DefaultProjectPath).Exists) {
                        globalConfigFile.Write("DefaultProjectPath", DefaultProjectPath, "System");
                    }
                } catch {
                }
            }
            globalConfigFile.Write("Author", Author, "System");

            globalConfigFile.DeleteSection("MruProjects");
            globalConfigFile.Write("MaxMruProjects", MaxMruProjects.ToString(), "MruProjects");

            var projectEnumerator = MruProjects.GetEnumerator();
            var i = 1;
            while (i <= MaxMruProjects && projectEnumerator.MoveNext()) {
                globalConfigFile.Write(i.ToString(), projectEnumerator.Current, "MruProjects");
                ++i;
            }

            IsModified = false;
        }
    }
}
