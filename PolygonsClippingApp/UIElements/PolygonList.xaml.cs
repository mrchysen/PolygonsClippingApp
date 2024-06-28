using PolygonsClippingApp.Models;
using PolygonsClippingApp.SubWindows;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace PolygonsClippingApp.UIElements
{
    /// <summary>
    /// Логика взаимодействия для PolygonList.xaml
    /// </summary>
    public partial class PolygonList : UserControl
    {
        public ObservableCollection<PolygonModel> Polygons { get; set; } = new();
        public Canvas Canvas { get; set; } = null!;
        public PolygonModel? SelectedModel { get; set; } 

        public PolygonList()
        {
            InitializeComponent();

            PolygonListBox.ItemsSource = Polygons;

            PolygonListBox.SelectionChanged += SelectionChanged;
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedModel = (PolygonModel)PolygonListBox.SelectedValue;
        }

        private void AddPolygon(object sender, RoutedEventArgs e)
        {
            PolygonWindow window = new();

            bool? dialogResult = window.ShowDialog();

            if (dialogResult != null && dialogResult.Value)
            {
                var model = window.GetPolygonModel;

                Polygons.Add(model);

                Canvas.Children.Add(model.Polygon);
            }
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

                Polygons[indexOfSelected] = model;

                Canvas.Children.Add(model.Polygon);
            }
        }
    }
}
