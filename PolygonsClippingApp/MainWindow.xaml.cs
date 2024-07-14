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
using Microsoft.Win32;
using System.Diagnostics;
using System.IO.Enumeration;

namespace PolygonsClippingApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string PathToFiles = FileNamingManager.BasicPathToFiles;

        public MainWindow()
        {
            InitializeComponent();

            PolygonList.Canvas = CanvasField;

            var res = IntersectionBase.IsPointInsidePoly(new Point(25,25), [new(0,0), new(50,50), new (0,50)]);

            MessageBox.Show(res.ToString());
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

        #region Saving button
        /// <summary>
        /// Обработчик кнопки - сохраняет все полигоны в файл.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton(object sender, RoutedEventArgs e)
        {
            try
            {
                var saveDialog = new SaveFileDialog();

                saveDialog.InitialDirectory = PathToFiles;
                saveDialog.DefaultExt = ".polys";
                saveDialog.FileName = "polygons";

                if (saveDialog.ShowDialog() == true)
                {
                    var fileName = saveDialog.FileName;

                    FileSaveManager.SaveToFile(PolygonList.Polygons, fileName);
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show($"Не получилось сохранить файл. Ошибка: {ex.Message}");
            }
        }
        #endregion

        #region Read-files button
        private void ReadButton(object sender, RoutedEventArgs e)
        { // Прочитать массив полигоны
            try
            { 
                IEnumerable<PolygonModel> polygonsFromFile = [];

                var readDialog = new OpenFileDialog();

                readDialog.InitialDirectory = PathToFiles;
                readDialog.DefaultExt = ".polys";
                readDialog.FileName = "polygons";

                if (readDialog.ShowDialog() == true)
                {
                    var fileName = readDialog.FileName;

                    polygonsFromFile = FileReadManager.ReadPolygonArrayFromFile(fileName);
                }

                foreach (var polygon in polygonsFromFile)
                {
                    PolygonList.Clear();
                    PolygonList.AddPolygon(polygon);
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show($"Не получилось считать файл. Ошибка: {ex.Message}");
            }
        }
        #endregion
    }
}