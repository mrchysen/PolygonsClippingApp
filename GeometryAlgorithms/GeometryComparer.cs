using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryAlgorithms;

/// <summary>
/// Класс - сравниватель точек с заданной погрешностью
/// </summary>
public class GeometryComparer
{
    public const double Eps = 0.0001d;

    public static bool IsEqual(double v1, double v2)
    {
        return Math.Abs(v1 - v2) < Eps;
    }
}
