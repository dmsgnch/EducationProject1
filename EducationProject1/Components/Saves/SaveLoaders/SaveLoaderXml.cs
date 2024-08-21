using System.IO;
using System.Windows;
using System.Xml.Serialization;
using EducationProject1.Components.Saves.SaveLoaders.Abstract;
using EducationProject1.Models.SaveObjects;
using MessagePack;

namespace EducationProject1.Components.Saves.SaveLoaders;

public class SaveLoaderXml : SaveLoaderBase
{
    public SaveLoaderXml() : base(".xml")
    { }
    
    protected override async Task<Save<T>?> GetSaveFromFileOrNullAsync<T>(string filePath) where T : class
    {
        try
        {
            var serializer = new XmlSerializer(typeof(Save<T>));
            using (var reader = new StreamReader(filePath))
            {
                return (Save<T>?)serializer.Deserialize(reader);
            }
        }
        catch (IOException ioEx)
        {
            Console.WriteLine($"IO error: {ioEx.Message}");
            MessageBox.Show("An error occurred while reading the file. Please try again.", "Error", MessageBoxButton.OK,
                MessageBoxImage.Error);
            return null;
        }
        catch (MessagePackSerializationException ex)
        {
            Console.WriteLine($"Deserialize error {ex.Message}");
            MessageBox.Show("This file is not correct save file, please try again!", "Error");
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
}