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
using FileManagement;
using GeometryAlgorithms.Intersections;
using GeometryAlgorithms.Models;
using System.IO;

namespace PolygonsClippingApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string PathToApp = AppDomain.CurrentDomain.BaseDirectory;

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

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        { // сохранить первый полигон
            FileSaveManager.SaveToFile(PolygonList.Polygons[0], System.IO.Path.Combine(PathToApp, "poly.txt"));
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        { // сохранить все
            FileSaveManager.SaveToFile(PolygonList.Polygons, System.IO.Path.Combine(PathToApp, "polys.txt"));
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        { // Прочитать все полигоны
            IEnumerable<PolygonModel> polygonsFromFile = [];
            try
            {
                polygonsFromFile = FileReadManager.ReadPolygonArrayFromFile(System.IO.Path.Combine(PathToApp, "polys.txt"));
            }
            catch
            {
                polygonsFromFile = [];
                MessageBox.Show("error");
            }

            foreach (var polygon in polygonsFromFile) 
            {
                PolygonList.AddPolygon(polygon);
            }
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        { // Прочитать полигон

        }
    }
}