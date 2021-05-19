using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projekt_Systemy
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            StartCar();
        }

        private void StartCar()
        {
            string enviroment = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string path = enviroment + "\\Projekt_systemy\\src";
            Image car = new Image();
            car.Source = new BitmapImage(new Uri("/src/car_blue.png" ,UriKind.Relative));
            car.Stretch = Stretch.Fill;
            car.Height =50;
            car.Width =50;
            canvas.Children.Add(car);
            Canvas.SetTop(car, 110);
            Canvas.SetLeft(car, 120);
        }
    }
}
