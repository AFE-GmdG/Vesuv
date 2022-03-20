using System.Globalization;
using System.IO;

using Vesuv.Core.Collections;
using Vesuv.Core.Config;
using Vesuv.Win32;

namespace Vesuv.Editor
{

    public class GlobalConfig : ConfigBase
    {
        private static GlobalConfig _instance = new GlobalConfig();
        public static GlobalConfig Instance => _instance;

        public string? GraphicDeviceName { get; set; }
        public string? DefaultProjectPath { get; set; }

        public uint MaxMruProjects { get; set; }
        public ICollection<string> MruProjects { get; }

        private GlobalConfig()
        {
            var globalConfigFilePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Vesuv",
                "Vesuv.ini");
            var fileInfo = new FileInfo(globalConfigFilePath);
            if (!fileInfo.Directory!.Exists) {
                var defaultProjectPathInfo = new DirectoryInfo(Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    "Vesuv"));
                if (!defaultProjectPathInfo.Exists) {
                    defaultProjectPathInfo.Create();
                }
                DefaultProjectPath = defaultProjectPathInfo.FullName;

                MaxMruProjects = 10;
                MruProjects = new MRU<string>(MaxMruProjects);
                ((MRU<string>)MruProjects).ItemsChanged += OnConfigChange;

                return;
            }

            var globalConfigFile = new IniFile(fileInfo.FullName);
            MaxMruProjects = UInt32.Parse(
                globalConfigFile.ReadDefault("MaxMruProjects", "10", "MruProjects"),
                NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite,
                CultureInfo.InvariantCulture);

            GraphicDeviceName = globalConfigFile.Read("GraphicDeviceName", "Video");

            DefaultProjectPath = globalConfigFile.Read("DefaultProjectPath", "System");
            if (DefaultProjectPath != null) {
                try {
                    if (!new DirectoryInfo(DefaultProjectPath).Exists) {
                        DefaultProjectPath = null;
                    }
                } catch {
                }
            }
            if (DefaultProjectPath == null) {
                var defaultProjectPathInfo = new DirectoryInfo(Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    "Vesuv"));
                if (!defaultProjectPathInfo.Exists) {
                    defaultProjectPathInfo.Create();
                }
                DefaultProjectPath = defaultProjectPathInfo.FullName;
            }

            var mruProjects = new List<string>((int)MaxMruProjects);
            var enumerationOptions = new EnumerationOptions();
            enumerationOptions.AttributesToSkip = FileAttributes.System | FileAttributes.Directory;
            enumerationOptions.IgnoreInaccessible = true;
            enumerationOptions.MatchCasing = MatchCasing.CaseInsensitive;
            enumerationOptions.MatchType = MatchType.Simple;
            enumerationOptions.RecurseSubdirectories = false;
            enumerationOptions.ReturnSpecialDirectories = false;

            for (uint i = 1; i <= MaxMruProjects; ++i) {
                var mruProject = globalConfigFile.Read(i.ToString(), "MruProjects");
                if (mruProject != null) {
                    try {
                        var projectInfo = new DirectoryInfo(mruProject);
                        if (!projectInfo.Exists) {
                            continue;
                        }
                        var projectConfigInfo = projectInfo.EnumerateFiles("project.vesuv", enumerationOptions).FirstOrDefault();
                        if (projectConfigInfo == null) {
                            continue;
                        }

                        // TODO: Check project...

                        mruProjects.Add(mruProject);
                    } catch {
                        continue;
                    }
                }
            }

            MruProjects = new MRU<string>(MaxMruProjects, mruProjects);
            ((MRU<string>)MruProjects).ItemsChanged += OnConfigChange;
        }

        public override void RevertChanges()
        {
            if (!IsModified) {
                return;
            }

            throw new NotImplementedException();
        }

        public override void SaveChanges()
        {
            var globalConfigFilePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Vesuv",
                "Vesuv.ini");
            var globalConfigFile = new IniFile(globalConfigFilePath);

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

            globalConfigFile.DeleteSection("MruProjects");
            globalConfigFile.Write("MaxMruProjects", MaxMruProjects.ToString(), "MruProjects");
            var projectEnumerator = MruProjects.GetEnumerator();
            var i = 1;
            while (i <= MaxMruProjects && projectEnumerator.MoveNext()) {
                globalConfigFile.Write(i.ToString(), projectEnumerator.Current, "MruProjects");
            }

            IsModified = false;
        }

    }

}
