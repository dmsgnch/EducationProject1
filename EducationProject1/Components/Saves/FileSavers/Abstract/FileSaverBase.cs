using EducationProject1.Localization.Resources;
using EducationProject1.Models.SaveObjects;
using Microsoft.Win32;

namespace EducationProject1.Components.Saves.FileSavers.Abstract;

public abstract class FileSaverBase
{
    protected abstract string FileFilter { get; init; }

    public async Task SaveInFile<T>(T objectToSave, string? filePath = null)
    {
        if (filePath is null) filePath = GetFilePathFromUserOrNull();
        if (filePath is not null)
        {
            var save = new Save<T>(objectToSave);

            await WriteSaveToFile(save, filePath);
        }
    }

    public virtual string? GetFilePathFromUserOrNull()
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog
        {
            Title = Resources.SelectPlaceForSavingFile,
            Filter = FileFilter
        };

        return saveFileDialog.ShowDialog() ?? false ? saveFileDialog.FileName : null;
    }
    
    protected abstract Task WriteSaveToFile<TSave>(TSave saveObject, string filePath);
}