using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GeometryAlgorithms.Intersections;
using GeometryAlgorithms.Models;

namespace PolygonsClippingApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            PolygonList.Canvas = CanvasField;
        }

        private void FindConvexIntersectionMenuItemClick(object sender, RoutedEventArgs e)
        {
            IntersectionBase intersection = new ConvexIntersection();

            var poly = PolygonList.Polygons.ToList();

            var intersectionPoly = intersection.FindIntersection(poly[0].GetPoints(), poly[1].GetPoints());

            PolygonList.AddPolygon(new PolygonModel()
            {
                Name = $"Полигон {PolygonList.Polygons.Count + 1}",
                Polygon = new Polygon()
                {
                    Points = new PointCollection(intersectionPoly),
                    Fill = new SolidColorBrush(Colors.Aquamarine)
                }
            });
        }
    }
}