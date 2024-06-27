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
                var poly = window.GetPolygonModel.Polygon;

                Polygons.Add(window.GetPolygonModel);

                Canvas.Children.Add(poly);
            }
        }

        private void DeletePolygon(object sender, RoutedEventArgs e)
        {
            if (SelectedModel != null) 
            {
                Polygons.Remove(SelectedModel);
            }
        }


    }
}
