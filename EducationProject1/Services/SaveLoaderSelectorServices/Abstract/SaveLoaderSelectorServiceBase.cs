using EducationProject1.Components.Saves.SaveLoaders.Abstract;

namespace EducationProject1.Services.SaveLoaderSelectorServices.Abstract;

public abstract class SaveLoaderSelectorServiceBase
{
    public abstract SaveLoaderBase GetSaveLoader(string filePath);
}