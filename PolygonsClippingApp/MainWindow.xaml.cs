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

namespace PolygonsClippingApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public double Zoom { get; set; } = 1.0d;

        public MainWindow()
        {
            InitializeComponent();
            Canvas.SizeChanged += BuildGridCanvas;
            Canvas.MouseWheel += MouseWheelAction;

            
        }

        protected void MouseWheelAction(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
                Zoom+=0.1d;

            else if (e.Delta < 0)
                Zoom -= 0.1d;

            Zoom = (Zoom <= 0.1) ? 0.1d : Zoom;

            BuildGridCanvas(this, null);
        }

        protected void BuildGridCanvas(object sender, EventArgs args)
        {
            Canvas.Children.Clear();
            
            double delta = 10.0d;

            double midX = Canvas.ActualWidth / 2;
            double midY = Canvas.ActualHeight / 2;

            Line middleLineX = new();

            middleLineX.Stroke = new SolidColorBrush(Colors.Black);
            middleLineX.StrokeThickness = 0.3;

            middleLineX.X1 = midX;
            middleLineX.X2 = midX;
            middleLineX.Y1 = 0;
            middleLineX.Y2 = Canvas.ActualHeight;

            Canvas.Children.Add(middleLineX);

            Line middleLineY = new();

            middleLineY.Stroke = new SolidColorBrush(Colors.Black);
            middleLineY.StrokeThickness = 0.3;

            middleLineY.X1 = 0;
            middleLineY.X2 = Canvas.ActualWidth;
            middleLineY.Y1 = midY;
            middleLineY.Y2 = midY;

            Canvas.Children.Add(middleLineY);


            for (double x = 0; x < Canvas.ActualWidth; x+=delta*Zoom) 
            {
                Line line = new();

                line.Stroke = new SolidColorBrush(Colors.Black);
                line.StrokeThickness = 0.1;

                line.X1 = x;
                line.X2 = x;
                line.Y1 = 0;
                line.Y2 = Canvas.ActualHeight;

                Canvas.Children.Add(line);
            }

            for (double y = 0; y < Canvas.ActualHeight; y += delta * Zoom)
            {
                Line line = new();

                line.Stroke = new SolidColorBrush(Colors.Black);
                line.StrokeThickness = 0.1;

                line.X1 = 0;
                line.X2 = Canvas.ActualWidth;
                line.Y1 = y;
                line.Y2 = y;

                Canvas.Children.Add(line);
            }
        }
    }
}