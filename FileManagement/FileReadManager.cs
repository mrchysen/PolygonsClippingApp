using GeometryAlgorithms.Models;
using System.Text.Json;
using System.Windows;
using System.IO;

namespace FileManagement;

public class FileReadManager
{
    public static PolygonModel ReadPolygonFromFile(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException();

        string json = File.ReadAllText(path);

        PolygonModel? polygon = new();

        try
        {
            polygon = JsonSerializer.Deserialize<PolygonModel>(json);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        return polygon ?? new();
    }
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
