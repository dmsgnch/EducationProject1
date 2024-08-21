using System.IO;
using System.Xml.Serialization;
using EducationProject1.Components.Saves.FileSavers.Abstract;

namespace EducationProject1.Components.Saves.FileSavers;

public class FileSaverXml : FileSaverBase
{
    protected override string FileFilter { get; init; } = "XML files (*.xml)|*.xml";
    
    protected override async Task WriteSaveToFile<TSave>(TSave saveObject, string filePath)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(TSave));
        
        await using (FileStream stream = new FileStream(filePath, FileMode.Create))
        {
            serializer.Serialize(stream, saveObject);
        }
    }
}