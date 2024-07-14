using GeometryAlgorithms.Models;
using System.Text.Json;
using System.Windows;
using System.IO;

namespace FileManagement;

public class FileReadManager
{
    public static IEnumerable<PolygonModel> ReadPolygonArrayFromFile(string path)
    {
        if (!File.Exists(path)) 
            throw new FileNotFoundException();

        string json = File.ReadAllText(path);

        List<PolygonModel>? polygons = [];

        try
        {
            polygons = JsonSerializer.Deserialize<List<PolygonModel>>(json);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        return polygons ?? [];
    }
}
