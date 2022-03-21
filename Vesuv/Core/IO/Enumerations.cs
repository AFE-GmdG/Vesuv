namespace Vesuv.Core.IO
{
    [Flags]
    public enum FileChangedReason : UInt32
    {
        Unknown          = 0x00000000,
        Path             = 0x00000001,
        Name             = 0x00000002,
        ModificationTime = 0x00000004,
        ResourceType     = 0x00000008,
        FileState        = 0x00000010,
        Content          = 0x10000000,
    }

    [Flags]
    public enum FileState : UInt32
    {
        New              = 0x00000000,
        Modified         = 0x00000001,
        Saved            = 0x00000002,
        Deleted          = 0x00000004,
        Readonly         = 0x10000000,
        Offline          = 0x40000000,
        Missing          = 0x80000000,
    }

    public enum ResourceType : UInt32
    {
        Unknown,
        Project,
        Scene2D,
        Scene3D,
        ScriptCS,
    }

    public enum Scheme : UInt32
    {
        Res,
        User,
    }
}
