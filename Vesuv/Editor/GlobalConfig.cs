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

        public string DefaultProjectPath { get; set; }
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

            DefaultProjectPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Vesuv");
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

            var defaultProjectPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Vesuv");
            DefaultProjectPath = globalConfigFile.ReadDefault("DefaultProjectPath", defaultProjectPath, "System");
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

            MruProjects = new MRU<string>(MaxMruProjects, mruProjects);
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
