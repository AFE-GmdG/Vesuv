using System.IO;

using Vesuv.Core.Config;

namespace Vesuv.Core
{

    public class ProjectFile : ConfigBase
    {

        public DateTime ModifikationTime { get; private set; }

        public ProjectFile(FileInfo projectFile)
        {
            if (!projectFile.Name.Equals("project.vesuv", StringComparison.InvariantCultureIgnoreCase) || !projectFile.Exists) {
                throw new ArgumentException("Invalid project file", nameof(projectFile));
            }
            ModifikationTime = projectFile.LastWriteTime;
        }

        public ProjectFile(DirectoryInfo projectDirectory)
        {
            if (projectDirectory.Exists) {
                projectDirectory.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly);
            } else {
                // Create a new Project
                ModifikationTime = DateTime.Now;
            }
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
            IsModified = false;
        }
    }
}
