using System.IO;
using EducationProject1.Components.Saves.FileSavers.Abstract;
using MessagePack;

namespace EducationProject1.Components.Saves.FileSavers;

public class FileSaverBinary : FileSaverBase
{
    protected override string FileFilter { get; init; } = "Binary files (*.bin)|*.bin";

    protected override async Task WriteSaveToFile<TSave>(TSave saveObject, string filePath)
    {
        byte[] saveBinary = MessagePackSerializer.Serialize(saveObject);
        await File.WriteAllBytesAsync(filePath, saveBinary);
    }
}