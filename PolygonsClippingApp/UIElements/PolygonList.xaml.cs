using GeometryAlgorithms.Models;
using PolygonsClippingApp.SubWindows;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace PolygonsClippingApp.UIElements
{
    /// <summary>
    /// Логика взаимодействия для PolygonList.xaml
    /// </summary>
    public partial class PolygonList : UserControl
    {
        public ObservableCollection<PolygonModel> Polygons { get; set; } = [];
        public ZoomCanvas Canvas { get; set; } = null!;
        protected PolygonModel? SelectedModel { get; set; } 

        public PolygonList()
        {
            InitializeComponent();

            PolygonListBox.ItemsSource = Polygons;

            PolygonListBox.SelectionChanged += SelectionChanged;
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedModel = (PolygonModel)PolygonListBox.SelectedValue;

            if (SelectedModel != null)
            {
                Canvas.Children.Remove(SelectedModel.Polygon);
                Canvas.Children.Add(SelectedModel.Polygon);
            }
        }

        private void AddPolygon(object sender, RoutedEventArgs e)
        {
            PolygonWindow window = new($"Полигон {Polygons.Count + 1}");

            bool? dialogResult = window.ShowDialog();

            if (dialogResult != null && dialogResult.Value)
            {
                var model = window.GetPolygonModel;

                AddPolygon(model);

                Canvas.UpdateElements();
            }
        }

        public void AddPolygon(PolygonModel model)
        {
            Polygons.Add(model);

            Canvas.Children.Add(model.Polygon);
        }

        private void DeletePolygon(object sender, RoutedEventArgs e)
        {
            if (SelectedModel != null) 
            {
                Canvas.Children.Remove(SelectedModel.Polygon);

                Polygons.Remove(SelectedModel);
            }
        }

        private void ChangePolygon(object sender, RoutedEventArgs e)
        {
            if (SelectedModel == null)
                return;

            PolygonWindow window = new(SelectedModel);

            bool? dialogResult = window.ShowDialog();

            if (dialogResult != null && dialogResult.Value)
            {
                var model = window.GetPolygonModel;

                int indexOfSelected = Polygons.IndexOf(SelectedModel);

                Canvas.Children.Remove(SelectedModel.Polygon);

                Canvas.Children.Add(model.Polygon);

                Polygons[indexOfSelected] = model;

                Canvas.UpdateElements();
            }
        }
    }
}
