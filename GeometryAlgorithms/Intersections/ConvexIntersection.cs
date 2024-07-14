using System.Diagnostics;
using System.Windows;

namespace GeometryAlgorithms.Intersections;

/// <summary>
/// Класс для нахождения пересечения выпуклых многоугольников.
/// </summary>
public class ConvexIntersection : IntersectionBase
{
    public override List<Point> FindIntersection(List<Point> p1, List<Point> p2)
    {
        List<Point> clippedCorners = new List<Point>();
        
        for (int i = 0; i < p1.Count; i++)
        {
            if (IsPointInsidePoly(p1[i], p2))
                clippedCorners.Add(p1[i]);
        }

        Debug.WriteLine(string.Join(" ", clippedCorners));

        for (int i = 0; i < p2.Count; i++)
        {
            if (IsPointInsidePoly(p2[i], p1))
                clippedCorners.Add(p2[i]);
        }
        Debug.WriteLine(string.Join(" ", clippedCorners));


        for (int i = 0, next = 1; i < p1.Count; i++, next = (i + 1 == p1.Count) ? 0 : i + 1)
        {
            clippedCorners.AddRange(GetIntersectionPoints(p1[i], p1[next], p2));
        }
        Debug.WriteLine(string.Join(" ", clippedCorners));

        return OrderClockwise(clippedCorners);
    }
}
