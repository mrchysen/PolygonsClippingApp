using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media;
using System.Text.Json.Serialization;

namespace GeometryAlgorithms.Models;

public class PolygonModel
{
    [JsonIgnore]
    public Polygon Polygon = new();
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("points")]
    public IEnumerable<Point> Points {
        get => Polygon.Points;
        set => Polygon.Points = new(value);
    }
    [JsonPropertyName("fill")]
    public Brush Fill { 
        get => Polygon.Fill;
        set => Polygon.Fill = value; 
    }
    [JsonPropertyName("stroke")]
    public Brush Stroke { 
        get => Polygon.Stroke; 
        set => Polygon.Stroke = value;
    }
    [JsonPropertyName("stroke_thickness")]
    public double StrokeThickness { 
        get => Polygon.StrokeThickness;
        set => Polygon.StrokeThickness = value;
    }
}
