using System.IO;
using EducationProject1.Components.Saves.SaveLoaders;
using EducationProject1.Components.Saves.SaveLoaders.Abstract;
using EducationProject1.Services.SaveLoaderSelectorServices.Abstract;

namespace EducationProject1.Services.SaveLoaderSelectorServices;

public class SaveLoaderSelectorService : SaveLoaderSelectorServiceBase
{
    public override SaveLoaderBase GetSaveLoader(string filePath)
    {
        var fileExtension = GetFileExtension(filePath);

        switch (fileExtension)
        {
            case ".bin": return new SaveLoaderBinary();
            case ".json": return new SaveLoaderJson();
            case ".xml": return new SaveLoaderXml();
                
            default:
                throw new NotSupportedException($"The file extension '{fileExtension}' is not supported.");
        }
    }

    private string GetFileExtension(string filePath)
    {
        return Path.GetExtension(filePath);
    }
}