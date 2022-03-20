using System.IO;
using System.Reflection;

namespace Vesuv.Core
{
    public class ProjectInfo
    {

        public bool IsMissing { get; set; }
        public string ProjectName { get; set; }
        public string? ProjectDescription { get; set; }
        public Version EngineVersion { get; set; }
        public DateTime ModifikationTime { get; set; }

        public ProjectInfo(string projectName, Version? engineVersion = null)
        {
            IsMissing = false;
            ProjectName = projectName;
            if (engineVersion != null) {
                EngineVersion = engineVersion;
            } else {
                EngineVersion = Common.CurrentEngineVersion;
            }
            ModifikationTime = DateTime.Now;
        }

        public ProjectInfo(DirectoryInfo projectPath, bool isMissing)
        {
            if (!isMissing) {
                throw new InvalidOperationException("THis ctor is only for missing projects.");
            }
            IsMissing = true;
            ProjectName = projectPath.Name;
            EngineVersion = new Version(0, 0, 0);
            ModifikationTime = projectPath.Exists
                ? projectPath.LastWriteTime
                : new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
        }

        public ProjectInfo(ProjectFile projectFile)
        {
            IsMissing = false;
            ProjectName = "";
            EngineVersion = Common.CurrentEngineVersion;
            ModifikationTime = projectFile.ModifikationTime;
        }
    }
}
