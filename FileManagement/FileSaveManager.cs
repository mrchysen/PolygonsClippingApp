using GeometryAlgorithms.Models;
using System.Text.Json;
using System.IO;
using System.Windows;

namespace FileManagement;

public static class FileSaveManager
{
    public static void SaveToFile(PolygonModel model, string path)
    {
        string json = JsonSerializer.Serialize(model);
        
        try
        {
            File.WriteAllText(path, json);
        }
        catch(Exception ex)
        {
            MessageBox.Show($"{ex.Message}","Error",MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    public static void SaveToFile(IEnumerable<PolygonModel> models, string path)
    {
        string json = JsonSerializer.Serialize(models);

        try
        {
            File.WriteAllText(path, json);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
