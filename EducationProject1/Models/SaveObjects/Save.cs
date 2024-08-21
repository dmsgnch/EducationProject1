using MessagePack;

namespace EducationProject1.Models.SaveObjects;

[MessagePackObject]
[Serializable]
public class Save<T>
{
    [Key(0)]
    public SaveHeader SaveHeader { get; set; }
    [Key(1)]
    public T SaveObject { get; set; }

    public Save()
    { }

    public Save(T saveObject)
    {
        SaveHeader = new SaveHeader(typeof(T).FullName);
        SaveObject = saveObject;
    }
}