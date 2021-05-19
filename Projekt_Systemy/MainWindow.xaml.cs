using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
           
        }

        private void StartCar()
        {
            int x = 50;
            while (true)
            {
                this.Dispatcher.Invoke((Action)(() =>
                {
                    MoveCar(x);
                }));
                System.Threading.Thread.Sleep(100);
                x++;
                if (x > 500)
                {
                    break;
                }
            }
        }

        void StartTrain()
        {
            int x = -370;
            while(true){
                this.Dispatcher.Invoke((Action)(() =>
                {
                    Canvas.SetTop(pociag, x);
                }));
                System.Threading.Thread.Sleep(2);
                x++;
                if (x > 600)
                {
                    break;
                }
            }
        }

        private void buttonStart(object sender, RoutedEventArgs e)
        {
            Thread iThread = new Thread(new ThreadStart(StartTrain));
            iThread.Start();
            Thread uThread = new Thread(new ThreadStart(StartCar));
            uThread.Start();
        }

        private void MoveCar(int x)
        {
            Canvas.SetLeft(car1, x);
        }
    }
}
