using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PolygonsClippingApp.Models;

namespace PolygonsClippingApp.SubWindows
{
    /// <summary>
    /// Логика взаимодействия для PolygonWindow.xaml
    /// </summary>
    public partial class PolygonWindow : Window
    {
        public PolygonModel? PolygonModel;

        public PolygonModel GetPolygonModel => new PolygonModel()
        {
            Name = NameTextBox.Text,
            Polygon = new()
            {
                Points = new PointCollection(GetPoints()),
                Fill = new SolidColorBrush(ColorPicker.SelectedColor),
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 1
            }
        };

        public PolygonWindow()
        {
            InitializeComponent();
        }

        public PolygonWindow(PolygonModel model)
        {
            InitializeComponent();

            PolygonModel = model;

            PointsTextBox.Text = PointsToString(model.Polygon.Points);
            ColorPicker.SelectedColor = (model.Polygon.Fill as SolidColorBrush ?? new SolidColorBrush(Colors.Black)).Color;
            NameTextBox.Text = model.Name;
        }

        public string PointsToString(PointCollection points)
        {
            StringBuilder sb = new();

            foreach (var point in points) 
            {
                sb.Append(point.X + "_" + point.Y + " ");
            }

            if(sb.Length >= 1)
                sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }

        protected IEnumerable<Point> GetPoints()
        {
            var collection = PointsTextBox.Text.Split().Select(el =>
            {
                var point = el.Split("_").Select(int.Parse);

                return new Point(point.First(), point.Last());
            });

            return collection;
        }

        private void Create(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
