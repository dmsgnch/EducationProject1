using System.IO;
using System.Windows;
using EducationProject1.Components.Saves.SaveLoaders.Abstract;
using EducationProject1.Models.SaveObjects;
using MessagePack;

namespace EducationProject1.Components.Saves.SaveLoaders;

public class SaveLoaderBinary : SaveLoaderBase
{
    public SaveLoaderBinary() : base(".bin")
    {
    }

    protected override async Task<Save<T>?> GetSaveFromFileOrNullAsync<T>(string filePath) where T : class
    {
        try
        {
            await using (var fileStream =
                         new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
            using (var memoryStream = new MemoryStream())
            {
                await fileStream.CopyToAsync(memoryStream);
                byte[] fileData = memoryStream.ToArray();
                return GetDeserializedFileDataOrNull<T>(fileData);
            }
        }
        catch (IOException ioEx)
        {
            Console.WriteLine($"IO error: {ioEx.Message}");
            MessageBox.Show(
                "An error occurred while reading the file. Please try again.", 
                "Error", 
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
            throw new Exception($"Unexpected error: {ex.Message}");
        }
    }

    private Save<T>? GetDeserializedFileDataOrNull<T>(byte[] fileData)
    {
        try
        {
            return MessagePackSerializer.Deserialize<Save<T>>(fileData);
        }
        catch (MessagePackSerializationException ex)
        {
            Console.WriteLine($"Deserialize error {ex.Message}");
            MessageBox.Show("This file is not correct save file, please try again!", "Error", 
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            return null;
        }
        catch (Exception ex)
        {
            throw new Exception($"Unexpected error: {ex.Message}");
        }
    }
}