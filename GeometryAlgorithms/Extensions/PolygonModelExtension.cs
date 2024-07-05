using System.Windows;
using GeometryAlgorithms.Models;

namespace PolygonsClippingApp;

public static class PolygonModelExtension
{
    public static List<Point> GetPoints(this PolygonModel polygonModel)
            => polygonModel.Polygon.Points.ToList();
}
