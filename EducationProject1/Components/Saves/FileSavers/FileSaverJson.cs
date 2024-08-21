using System.IO;
using EducationProject1.Components.Saves.FileSavers.Abstract;
using Newtonsoft.Json;

namespace EducationProject1.Components.Saves.FileSavers;

public class FileSaverJson : FileSaverBase
{
    protected override string FileFilter { get; init; } = "JSON files (*.json)|*.json";
    
    protected override async Task WriteSaveToFile<TSave>(TSave saveObject, string filePath)
    {
        string saveJson = JsonConvert.SerializeObject(saveObject);
        await File.WriteAllTextAsync(filePath, saveJson);
    }
}