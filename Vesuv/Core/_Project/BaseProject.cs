using System.IO;

namespace Vesuv.Core._Project
{
    public abstract class BaseProject : IProject
    {
        public abstract bool IsReadonly { get; }
        public abstract bool IsMissing { get; }
        public abstract bool IsModified { get; }
        public abstract DirectoryInfo? ProjectDirectory { get; }
        public abstract string Name { get; set; }
        public abstract string? Description { get; set; }
        public abstract string? Author { get; set; }
        public abstract Version? ProjectVersion { get; set; }
        public abstract Version EngineVersion { get; }

        public abstract event EventHandler? ProjectChanged;
        public abstract event EventHandler? ProjectSaved;
        public abstract event EventHandler? ProjectReverted;

        public abstract bool Equals(IProject? other);
    }
}
