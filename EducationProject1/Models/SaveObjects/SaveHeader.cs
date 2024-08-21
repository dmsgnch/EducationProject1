using EducationProject1.Components.Saves;
using MessagePack;

namespace EducationProject1.Models.SaveObjects;

[MessagePackObject]
[Serializable]
public class SaveHeader
{
    [Key(0)] public string Signature { get; set; }
    [Key(1)] public SaveVersion Version { get; set; }
    [Key(2)] public string? DataType { get; set; }

    public SaveHeader()
    { }

    public SaveHeader(string dataType, string? signature = null, SaveVersion? version = null)
    {
        DataType = dataType;
        Signature = signature ?? SaveInfo.Signature;
        Version = version ?? SaveInfo.ActualVersion;
    }
}