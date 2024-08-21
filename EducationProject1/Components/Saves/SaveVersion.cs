using MessagePack;

namespace EducationProject1.Components.Saves;

[MessagePackObject]
public class SaveVersion
{
    [Key(0)]
    public int Major { get; set; }
    [Key(1)]
    public int Minor { get; set; }
    [Key(2)]
    public int Patch { get; set; }

    public SaveVersion()
    { }

    public SaveVersion(int major, int minor, int patch)
    {
        Major = major;
        Minor = minor;
        Patch = patch;
    }

    public override string ToString()
    {
        return $"{Major}.{Minor}.{Patch}";
    }
}