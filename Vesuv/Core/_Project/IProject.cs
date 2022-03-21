using System.IO;

namespace Vesuv.Core._Project
{
    public interface IProject : IEquatable<IProject>
    {
        bool IsReadonly { get; }
        bool IsMissing { get; }
        bool IsModified { get; }

        DirectoryInfo? ProjectDirectory { get; }

        string Name { get; set; }
        string? Description { get; set; }
        string? Author { get; set; }
        Version? ProjectVersion { get; set; }
        Version EngineVersion { get; }

        event EventHandler? ProjectChanged;
        event EventHandler? ProjectSaved;
        event EventHandler? ProjectReverted;

    }
}
