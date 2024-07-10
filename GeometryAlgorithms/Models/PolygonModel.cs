using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media;
using System.Text.Json.Serialization;
using GeometryAlgorithms.Models.JSONConverters;

namespace GeometryAlgorithms.Models;

public class PolygonModel
{
    [JsonIgnore]
    public Polygon Polygon = new();
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("points")]
    public IEnumerable<Point> Points {
        get => Polygon.Points;
        set => Polygon.Points = new(value);
    }
    [JsonPropertyName("fill")]
    [JsonConverter(typeof(SolidColorBrushConverter))]
    public SolidColorBrush Fill { 
        get => (SolidColorBrush)Polygon.Fill;
        set => Polygon.Fill = value; 
    }
    [JsonPropertyName("stroke")]
    [JsonConverter(typeof(SolidColorBrushConverter))]
    public SolidColorBrush Stroke { 
        get => (SolidColorBrush)Polygon.Stroke; 
        set => Polygon.Stroke = value;
    }
    [JsonPropertyName("stroke_thickness")]
    public double StrokeThickness { 
        get => Polygon.StrokeThickness;
        set => Polygon.StrokeThickness = value;
    }
}
