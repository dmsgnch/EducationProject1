using System.IO;
using System.Windows;
using EducationProject1.Components.Saves.SaveLoaders.Abstract;
using EducationProject1.Models.SaveObjects;
using MessagePack;
using Newtonsoft.Json;

namespace EducationProject1.Components.Saves.SaveLoaders;

public class SaveLoaderJson : SaveLoaderBase
{
    public SaveLoaderJson() : base(".json")
    {
    }

    protected override async Task<Save<T>?> GetSaveFromFileOrNullAsync<T>(string filePath) where T : class
    {
        try
        {
            string jsonData = await File.ReadAllTextAsync(filePath);
            return GetDeserializedFileDataOrNull<T>(jsonData);
        }
        catch (IOException ioEx)
        {
            Console.WriteLine($"IO error: {ioEx.Message}");
            MessageBox.Show("An error occurred while reading the file. Please try again.", "Error", MessageBoxButton.OK,
                MessageBoxImage.Error);
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
            MessageBox.Show("An unexpected error occurred. Please try again.", "Error", MessageBoxButton.OK,
                MessageBoxImage.Error);
            return null;
        }
    }

    private Save<T>? GetDeserializedFileDataOrNull<T>(string fileData)
    {
        try
        {
            return JsonConvert.DeserializeObject<Save<T>>(fileData);
        }
        catch (MessagePackSerializationException ex)
        {
            Console.WriteLine($"Deserialize error {ex.Message}");
            MessageBox.Show("This file is not correct save file, please try again!", "Error");
            return null;
        }
        catch (Exception ex)
        {
            throw new Exception($"Unexpected error: {ex.Message}");
        }
    }
}