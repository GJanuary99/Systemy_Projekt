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
        Random rand = new Random();
        const int minTrainTime = 5000;
        const int maxTrainTime = 20000;
        bool lightSignal = false;

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
            while (true)
            {
                Thread.Sleep(rand.Next(minTrainTime, maxTrainTime));
                lightSignal = true;
                int x = -370;
                while (true)
                {
                    
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
                lightSignal = false;
            }
        }

        void StartLights()
        {
            bool rightLight = true;
            while (true)
            {
                Thread.Sleep(300);
                this.Dispatcher.Invoke((Action)(() =>
                    {
                        if (lightSignal)
                        {
                            if (rightLight)
                            {
                                light.Source = new BitmapImage(new Uri("/src/car_yellow.png", UriKind.Relative));
                                rightLight = !rightLight;
                            }
                            else
                            {
                                light.Source = new BitmapImage(new Uri("/src/car_red.png", UriKind.Relative));
                                rightLight = !rightLight;
                            }
                        }
                        else
                        {
                            light.Source = new BitmapImage(new Uri("/src/car_blue.png", UriKind.Relative));
                        }
                    }));
            }
        }

        private void buttonStart(object sender, RoutedEventArgs e)
        {
            Thread tThread = new Thread(new ThreadStart(StartTrain));
            tThread.IsBackground = true;
            tThread.Start();
            Thread cThread = new Thread(new ThreadStart(StartCar));
            cThread.IsBackground = true;
            cThread.Start();
            Thread lThread = new Thread(new ThreadStart(StartLights));
            lThread.IsBackground = true;
            lThread.Start();
        }

        private void MoveCar(int x)
        {
            Canvas.SetLeft(car1, x);
        }
    }
}
