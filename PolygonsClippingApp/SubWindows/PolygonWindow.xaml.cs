using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using GeometryAlgorithms.Models;
using GeometryAlgorithms.Intersections;

namespace PolygonsClippingApp.SubWindows
{
    /// <summary>
    /// Логика взаимодействия для PolygonWindow.xaml
    /// Окно для создания/редактирования полигона.
    /// </summary>
    public partial class PolygonWindow : Window
    {
        // Поля \\
        public PolygonModel? PolygonModel;
        #region Конструкторы
        public PolygonWindow(string defaultText)
        {
            InitializeComponent();

            NameTextBox.Text = defaultText;

            PointsTextBox.Text = "1_1 1_200 200_1";

            SubmitButton.Content = "Создать";
        }
        public PolygonWindow(PolygonModel model)
        {
            InitializeComponent();

            PolygonModel = model;

            PointsTextBox.Text = PointsToString(model.Polygon.Points);
            ColorPicker.SelectedColor = (model.Polygon.Fill as SolidColorBrush ?? new SolidColorBrush(Colors.Black)).Color;
            NameTextBox.Text = model.Name;

            SubmitButton.Content = "Изменить";
        }
        #endregion
        
        /// <summary>
        /// Возвращает настроенный в окошке полигон
        /// </summary>
        public PolygonModel GetPolygonModel => new()
        {
            Name = NameTextBox.Text,
            Polygon = new()
            {
                Points = new PointCollection(IntersectionBase.OrderClockwise(Points.ToList())),
                Fill = new SolidColorBrush(ColorPicker.SelectedColor),
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 1
            }
        };

        /// <summary>
        /// Возвращает из коллекцию точек из строки PointsTextBox.
        /// </summary>
        protected IEnumerable<Point> Points
            => from splitStr in PointsTextBox.Text.Split()
               let point = splitStr.Split("_").Select(double.Parse)
               select new Point(point.First(), point.Last());

        /// <summary>
        /// Возвращает строку точек из данный PointCollection в виде;
        /// X1_Y1 X2_Y2 X3_Y3 ...
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        protected string PointsToString(PointCollection points)
            => string.Join(" ", points.Select(point => $"{point.X}_{point.Y}"));

        /// <summary>
        /// Нажатие на SubmitButton.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Create(object sender, RoutedEventArgs e)
        {
            if (IsValidPoints()) // Если точки валидны, то заканчиваем диалог
            {
                DialogResult = true;
                Close();
                return;
            }

            // Иначе просим корректно ввести
            WarningSpanPoints.Inlines.Clear();
            WarningSpanPoints.Inlines.Add(new Run("Некорректный ввод"));
            
        }

        /// <summary>
        /// Валидация массива точек из текста
        /// </summary>
        /// <returns></returns>
        protected bool IsValidPoints() 
        => PointsTextBox.Text.Split().All(el =>
            {
                var nums = el.Split("_");

                if (nums.Length != 2) return false;

                var flag1 = double.TryParse(nums.First(), out double num1);
                var flag2 = double.TryParse(nums.Last(), out double num2);

                return flag1 && flag2;
            });
    }
}
