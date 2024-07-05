using System.Windows;
using static GeometryAlgorithms.GeometryComparer;


namespace GeometryAlgorithms.Intersections;

public abstract class IntersectionBase
{
    public abstract List<Point> FindIntersection(List<Point> p1, List<Point> p2);

    /// <summary>
    /// Нахождение точки пересечения двух прямых.
    /// p1 и p2 задают 1-ю прямую.
    /// p3 и p4 задают 2-ю прямую.
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="p3"></param>
    /// <param name="p4"></param>
    /// <returns></returns>
    protected Point? GetIntersectionOfLines(Point p1, Point p2, Point p3, Point p4)
    {
        double A1 = p2.Y - p1.Y;
        double B1 = p1.X - p2.X;
        double C1 = A1 * p1.X + B1 * p1.Y;
        double A2 = p4.Y - p3.Y;
        double B2 = p3.X - p4.X;
        double C2 = A2 * p3.X + B2 * p3.Y;

        // Определитель нормалей 
        double det = A1 * B2 - A2 * B1;
        
        if (!IsEqual(det, 0d))
        {   // Если определитель не равен 0, то прямые не параллельны
            // Пересение прямых - можно найти с помощью СЛАУ 2х2 (решение методом крамера)
            double x = (B2 * C1 - B1 * C2) / det;
            double y = (A1 * C2 - A2 * C1) / det;
            
            bool cond1 = ((Math.Min(p1.X, p2.X) < x || IsEqual(Math.Min(p1.X, p2.X), x))
                && (Math.Max(p1.X, p2.X) > x || IsEqual(Math.Max(p1.X, p2.X), x))
                && (Math.Min(p1.Y, p2.Y) < y || IsEqual(Math.Min(p1.Y, p2.Y), y))
                && (Math.Max(p1.Y, p2.Y) > y || IsEqual(Math.Max(p1.Y, p2.Y), y))
                );
            bool cond2 = ((Math.Min(p3.X, p4.X) < x || IsEqual(Math.Min(p3.X, p4.X), x))
                && (Math.Max(p3.X, p4.X) > x || IsEqual(Math.Max(p3.X, p4.X), x))
                && (Math.Min(p3.Y, p4.Y) < y || IsEqual(Math.Min(p3.Y, p4.Y), y))
                && (Math.Max(p3.Y, p4.Y) > y || IsEqual(Math.Max(p3.Y, p4.Y), y))
                );
            if (cond1 && cond2) 
                return new Point(x, y);
        }

        return null; // прямые параллельны
    }

    /// <summary>
    /// Метод луча для проверки принадлежности точки полигону.
    /// </summary>
    /// <param name="p"></param>
    /// <param name="polygon"></param>
    /// <returns></returns>
    protected bool IsPointInsidePoly(Point p, List<Point> polygon)
    {
        int i;
        int j;
        bool result = false;
        for (i = 0, j = polygon.Count - 1; i < polygon.Count; j = i++)
        {
            if ((polygon[i].Y > p.Y) != (polygon[j].Y > p.Y) &&
                (p.X < (polygon[j].X - polygon[i].X) * (p.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) + polygon[i].X))
            {
                result = !result;
            }
        }
        return result;
    }

    /// <summary>
    /// Получаем лист точек, состоящий из точек, лежащих на прямой P1P2 и на стороне многоугольника.
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="polygon"></param>
    /// <returns></returns>
    protected List<Point> GetIntersectionPoints(Point p1, Point p2, List<Point> polygon)
    {
        List<Point> intersectionPoints = new List<Point>();

        for (int i = 0; i < polygon.Count; i++)
        {
            int next = (i + 1 == polygon.Count) ? 0 : i + 1;

            Point? point = GetIntersectionOfLines(p1, p2, polygon[i], polygon[next]);

            if (point != null) intersectionPoints.Add(point.Value);
        }

        return intersectionPoints;
    }

    protected List<Point> OrderClockwise(List<Point> points)
    {
        double mX = 0;
        double my = 0;

        foreach (Point p in points)
        {
            mX += p.X;
            my += p.Y;
        }

        mX /= points.Count;
        my /= points.Count;

        return points.OrderBy(v => Math.Atan2(v.Y - my, v.X - mX)).ToList();
    }
}
