using System.IO;
using System.Windows;
using EducationProject1.Models.SaveObjects;

namespace EducationProject1.Components.Saves.SaveLoaders.Abstract;

public abstract class SaveLoaderBase
{
    protected string FileTypeWorkWith { get; set; }

    protected SaveLoaderBase(string fileTypeWorkWith)
    {
        FileTypeWorkWith = fileTypeWorkWith;
    }

    public virtual async Task<T?> GetSaveDataOrNullAsync<T>(string filePath) where T : class
    {
        if (!IsCompatibleFileFormat(filePath))
            throw new NotSupportedException(
                $"This save loader does not support {Path.GetExtension(filePath)} file format");

        var save = await GetSaveFromFileOrNullAsync<T>(filePath);
        if (save is null) return null;

        if (!IsSaveHeaderCorrectSignature(save.SaveHeader.Signature))
        {
            MessageBox.Show(
                Localization.Resources.Resources.MessageIncorrectFileSignature, 
                Localization.Resources.Resources.CaptionError, 
                MessageBoxButton.OK, 
                MessageBoxImage.Error);
            return null;
        }

        if (!IsSaveHeaderCorrectVersion(save.SaveHeader.Version))
        {
            MessageBox.Show(
                Localization.Resources.Resources.MessageIncorrectSaveFileVersion, 
                Localization.Resources.Resources.CaptionError, 
                MessageBoxButton.OK, 
                MessageBoxImage.Error);
            return null;
        }

        return save.SaveObject;
    }

    protected abstract Task<Save<T>?> GetSaveFromFileOrNullAsync<T>(string filePath) where T : class;

    #region Checkers
    
    protected bool IsSaveHeaderCorrectSignature(string signature)
    {
        return signature.Equals(SaveInfo.Signature);
    }
    protected bool IsSaveHeaderCorrectVersion(SaveVersion saveVersion)
    {
        return SaveInfo.IsCompatibleVersion(saveVersion);
    }
    protected bool IsCompatibleFileFormat(string filePath)
    {
        return Path.GetExtension(filePath).Equals(FileTypeWorkWith);
    }
    
    #endregion
}