namespace Vesuv.Core.Project
{
    public interface IProject
    {
        bool IsReadonly { get; }
        bool IsMissing { get; }
        bool IsModified { get; }

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
